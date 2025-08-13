using AutoMapper;
using TestTask.Application.DTOs.Directories;
using TestTask.Application.DTOs.Warehouse;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Application.Interfaces.Services;
using TestTask.Application.Services;
using TestTask.Domain.Entities.Warehouse;
using TestTask.Domain.Enums;
using System;
using Microsoft.EntityFrameworkCore;

public class IncomingDocumentService
    : GenericService<IncomingDocument, IncomingDocumentDTO>, IIncomingDocumentService
{
    public IncomingDocumentService(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, unitOfWork.IncomingDocuments, mapper)
    {
    }

    public async Task<List<IncomingDocumentDTO>> GetFilteredAsync(
        List<string>? numbers,
        List<Guid>? resourceIds,
        List<Guid>? unitIds,
        DateTime? startDate,
        DateTime? endDate,
        CancellationToken cancellationToken)
    {
        var filteredDocuments = await _unitOfWork.IncomingDocuments.GetFilteredAsync(
            numbers,
            resourceIds,
            unitIds,
            startDate,
            endDate,
            cancellationToken);

        return _mapper.Map<List<IncomingDocumentDTO>>(filteredDocuments);
    }

    public async Task<FiltersData> GetFiltersDataAsync(CancellationToken cancellationToken = default)
    {
        var units = await _unitOfWork.Units.GetByStateAsync(DirectoriesStateEnum.Used, cancellationToken);
        var resources = await _unitOfWork.Resources.GetByStateAsync(DirectoriesStateEnum.Used, cancellationToken);
        var incomingDocuments = await _unitOfWork.IncomingDocuments.GetAllAsync(cancellationToken);

        return new FiltersData
        {
            unitDTOs = _mapper.Map<List<UnitDTO>>(units),
            ResourceDTOs = _mapper.Map<List<ResourceDTO>>(resources),
            incomingDocumentDTOs = _mapper.Map<List<IncomingDocumentDTO>>(incomingDocuments)
        };
    }

    public override async Task AddAsync(IncomingDocumentDTO incomingDocumentDTO, CancellationToken cancellationToken = default)
    {
        var incomingDocument = _mapper.Map<IncomingDocument>(incomingDocumentDTO);
        await _unitOfWork.IncomingDocuments.AddAsync(incomingDocument, cancellationToken);
        await UpdateBalanceOnCreateAsync(incomingDocument, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public override async Task<IncomingDocumentDTO> UpdateAsync(IncomingDocumentDTO incomingDocumentDTO, CancellationToken cancellationToken = default)
    {
        var existingDocument = await _unitOfWork.IncomingDocuments
             .GetByIdWithResourcesAsync(incomingDocumentDTO.Id!.Value, cancellationToken);

        var existingDocumentDTO = _mapper.Map<List<IncomingResourceDTO>>(existingDocument.IncomingResources);
        foreach (var oldIncomingResource in existingDocument.IncomingResources)
        {
            await _unitOfWork.IncomingResources.DeleteAsync(oldIncomingResource.Id, cancellationToken);
        }
        var newIncomingDocument = _mapper.Map<IncomingDocument>(incomingDocumentDTO);
        await UpdateBalanceOnUpdateAsync(existingDocumentDTO, incomingDocumentDTO.IncomingResourcesDTO, cancellationToken);
        await _unitOfWork.IncomingDocuments.UpdateAsync(newIncomingDocument, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return incomingDocumentDTO;
    }

    private async Task UpdateBalanceOnUpdateAsync(List<IncomingResourceDTO> oldResources, List<IncomingResourceDTO> newResources, CancellationToken cancellationToken)
    {
        var oldGrouped = oldResources
            .GroupBy(r => (r.ResourceDTO.Id, r.UnitDTO.Id))
            .ToDictionary(g => g.Key, g => g.Sum(r => r.Quantity));

        var newGrouped = newResources
            .GroupBy(r => (r.ResourceDTO.Id, r.UnitDTO.Id))
            .ToDictionary(g => g.Key, g => g.Sum(r => r.Quantity));


        var allKeys = oldGrouped.Keys.Union(newGrouped.Keys);

        foreach (var key in allKeys)
        {
            oldGrouped.TryGetValue(key, out var oldQty);
            newGrouped.TryGetValue(key, out var newQty);

            var diff = newQty - oldQty;

            if (diff == 0)
                continue;

            var balance = await FindBalanceAsync(key.Item1.Value, key.Item2.Value, cancellationToken);

            if (balance == null)
            {
                if (diff < 0)
                    throw new Exception("Недостаточно баланса для уменьшения при обновлении");

                balance = new Balance
                {
                    Id = Guid.NewGuid(),
                    Resource = await _unitOfWork.Resources.GetByIdAsync(key.Item1.Value, cancellationToken),
                    Unit = await _unitOfWork.Units.GetByIdAsync(key.Item2.Value, cancellationToken),
                    Quantity = 0
                };
                await _unitOfWork.Balances.AddAsync(balance, cancellationToken);
            }

            if (balance.Quantity + diff < 0)
                throw new Exception($"Недостаточно ресурса {key.Item1} для обновления");

            balance.Quantity += diff;
        }
    }

    private async Task UpdateBalanceOnCreateAsync(IncomingDocument doc, CancellationToken cancellationToken)
    {
        foreach (var incomingResources in doc.IncomingResources)
        {
            var balance = await FindBalanceAsync(incomingResources.Resource.Id, incomingResources.Unit.Id, cancellationToken);
            if (balance == null)
            {
                balance = new Balance
                {
                    Id = Guid.NewGuid(),
                    Resource = incomingResources.Resource,
                    Unit = incomingResources.Unit,
                    Quantity = 0
                };
                await _unitOfWork.Balances.AddAsync(balance, cancellationToken);
            }
            balance.Quantity += incomingResources.Quantity;
        }
    }

    private async Task CheckResourcesAndUnits(List<IncomingResourceDTO> incomingResourcesDTo, CancellationToken cancellationToken)
    {

        foreach (var incomingResourceDTO in incomingResourcesDTo)
        {
            var incomingResources = await _unitOfWork.IncomingResources.GetByIdAsNoTrackingAsync(incomingResourceDTO.ResourceDTO.Id!.Value, cancellationToken);

            var unitEntity = await _unitOfWork.Units.GetByIdAsync(incomingResourceDTO.UnitDTO.Id!.Value, cancellationToken);

            if (incomingResources.Resource == null || incomingResources.Resource.State == DirectoriesStateEnum.Archived)
                throw new Exception($"Resource {incomingResourceDTO.ResourceDTO.Id} не найден или архивирован");
            if (incomingResources.Unit == null || incomingResources.Unit.State == DirectoriesStateEnum.Archived)
                throw new Exception($"Unit {incomingResourceDTO.UnitDTO.Id} не найден или архивирован");
        }
    }   

    public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.IncomingDocuments.GetByIdAsync(id, cancellationToken);
        if (entity == null)
            throw new Exception("Документ поступления не найден");

        await UpdateBalanceOnDeleteAsync(entity, cancellationToken);

        await _unitOfWork.IncomingDocuments.DeleteAsync(entity.Id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateBalanceOnDeleteAsync(IncomingDocument doc, CancellationToken cancellationToken)
    {
        foreach (var resource in doc.IncomingResources)
        {
            var balance = await FindBalanceAsync(resource.Resource.Id, resource.Unit.Id, cancellationToken);
            if (balance == null || balance.Quantity < resource.Quantity)
                throw new Exception($"Недостаточно ресурса {resource.Resource.Id} для удаления");

            balance.Quantity -= resource.Quantity;
        }
    }

    private async Task<Balance?> FindBalanceAsync(Guid resourceId, Guid unitId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Balances.GetByResourceAndUnitAsync(resourceId, unitId, cancellationToken);
    }
}

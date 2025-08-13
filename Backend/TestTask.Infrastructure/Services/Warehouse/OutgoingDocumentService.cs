using AutoMapper;
using TestTask.Application.DTOs.Directories;
using TestTask.Application.DTOs.Warehouse;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Application.Interfaces.Services;
using TestTask.Application.Services;
using TestTask.Domain.Entities.Warehouse;
using TestTask.Domain.Enums;

namespace TestTask.Infrastructure.Services.Warehouse
{
    public class OutgoingDocumentService : GenericService<OutgoingDocument, OutgoingDocumentDTO>, IOutgoingDocumentService
    {
        public OutgoingDocumentService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, unitOfWork.OutgoingDocuments, mapper)
        {
        }

        public async Task<List<OutgoingDocumentDTO>> GetFilteredAsync(
           List<string>? numbers,
           List<Guid>? resourceIds,
           List<Guid>? unitIds,
           List<Guid>? clientIds,
           DateTime? startDate,
           DateTime? endDate,
           CancellationToken cancellationToken)
        {
            var filteredDocuments = await _unitOfWork.OutgoingDocuments.GetFilteredAsync(
                numbers,
                resourceIds,
                unitIds,
                clientIds,
                startDate,
                endDate,
                cancellationToken);

            return _mapper.Map<List<OutgoingDocumentDTO>>(filteredDocuments);
        }
        public override async Task<OutgoingDocumentDTO> UpdateAsync(OutgoingDocumentDTO outgoingDocumentDTO, CancellationToken cancellationToken = default)
        {
            var existingDocument = await _unitOfWork.OutgoingDocuments
                .GetByIdWithResourcesAsync(outgoingDocumentDTO.Id!.Value, cancellationToken);

            // Удаляем старые ресурсы
            foreach (var oldOutgoingResource in existingDocument.OutgoingResources)
            {
                await _unitOfWork.OutgoingResources.DeleteAsync(oldOutgoingResource.Id, cancellationToken);
            }

            // Маппим новый документ
            var newOutgoingDocument = _mapper.Map<OutgoingDocument>(outgoingDocumentDTO);

            await _unitOfWork.OutgoingDocuments.UpdateAsync(newOutgoingDocument, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return outgoingDocumentDTO;
        }



        public async Task<FiltersData> GetFiltersDataAsync(CancellationToken cancellationToken = default)
        {
            var units = await _unitOfWork.Units.GetByStateAsync(DirectoriesStateEnum.Used, cancellationToken);
            var resources = await _unitOfWork.Resources.GetByStateAsync(DirectoriesStateEnum.Used, cancellationToken);
            var clients = await _unitOfWork.Clients.GetByStateAsync(DirectoriesStateEnum.Used, cancellationToken);
            var outgoingDocuments = await _unitOfWork.OutgoingDocuments.GetAllAsync(cancellationToken);

            return new FiltersData
            {
                unitDTOs = _mapper.Map<List<UnitDTO>>(units),
                ResourceDTOs = _mapper.Map<List<ResourceDTO>>(resources),
                ClientDTOs = _mapper.Map<List<ClientDTO>>(clients),
                outgoingDocumentDTOs = _mapper.Map<List<OutgoingDocumentDTO>>(outgoingDocuments)
            };
        }

        public async Task SignAsync(Guid outgoingDocumentId, CancellationToken cancellationToken = default)
        {
            var document = await _unitOfWork.OutgoingDocuments.GetByIdAsync(outgoingDocumentId, cancellationToken);
            ValidateDocumentState(document, OutgoingStateEnum.unsigned, "Документ уже подписан");
            await AdjustBalanceAsync(document.OutgoingResources, isSubtract: true, cancellationToken);
            document.State = OutgoingStateEnum.signed;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task RevokeAsync(Guid outgoingDocumentId, CancellationToken cancellationToken = default)
        {
            var document = await _unitOfWork.OutgoingDocuments.GetByIdAsync(outgoingDocumentId, cancellationToken);
            ValidateDocumentState(document, OutgoingStateEnum.signed, "Документ не подписан");
            await AdjustBalanceAsync(document.OutgoingResources, isSubtract: false, cancellationToken);
            document.State = OutgoingStateEnum.unsigned;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task<OutgoingDocumentDTO> CreateAndSignAsync(OutgoingDocumentDTO outgoingDocumentDTO, CancellationToken cancellationToken = default)
        {
            var document = _mapper.Map<OutgoingDocument>(outgoingDocumentDTO);
            await _unitOfWork.OutgoingDocuments.AddAsync(document, cancellationToken);
            await SignAsync(document.Id, cancellationToken);
            return _mapper.Map<OutgoingDocumentDTO>(document);
        }
        public async Task<OutgoingDocumentDTO> UpdateAndSignAsync(OutgoingDocumentDTO outgoingDocumentDTO, CancellationToken cancellationToken = default)
        {
            var document = _mapper.Map<OutgoingDocument>(outgoingDocumentDTO);
            await UpdateAsync(outgoingDocumentDTO, cancellationToken);
            await SignAsync(document.Id, cancellationToken);
            return _mapper.Map<OutgoingDocumentDTO>(document);
        }

        private void ValidateDocumentState(OutgoingDocument document, OutgoingStateEnum expectedState, string errorMessage)
        {
            if (document.State != expectedState)
                throw new Exception(errorMessage);
        }

        private async Task AdjustBalanceAsync(IEnumerable<OutgoingResource> resources, bool isSubtract, CancellationToken cancellationToken)
        {
            foreach (var resource in resources)
            {
                var balance = await FindBalanceAsync(resource.Resource.Id, resource.Unit.Id, cancellationToken);

                if (balance == null)
                {
                    if (isSubtract)
                        throw new Exception($"Недостаточно ресурса {resource.Resource.Id} для подписания документа");

                    balance = new Balance
                    {
                        Id = Guid.NewGuid(),
                        Resource = resource.Resource,
                        Unit = resource.Unit,
                        Quantity = 0
                    };
                    await _unitOfWork.Balances.AddAsync(balance, cancellationToken);
                }

                var newQuantity = isSubtract ? balance.Quantity - resource.Quantity : balance.Quantity + resource.Quantity;

                if (newQuantity < 0)
                    throw new Exception($"Недостаточно ресурса {resource.Resource.Id} для подписания документа");

                balance.Quantity = newQuantity;
            }
        }

        private async Task<Balance?> FindBalanceAsync(Guid resourceId, Guid unitId, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Balances.GetByResourceAndUnitAsync(resourceId, unitId, cancellationToken);
        }

    }
}

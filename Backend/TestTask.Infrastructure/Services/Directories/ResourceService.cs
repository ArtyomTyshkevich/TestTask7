using AutoMapper;
using System.Threading;
using TestTask.Application.DTOs.Directories;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Application.Interfaces.Services;
using TestTask.Application.Services;
using TestTask.Domain.Entities.Directories;
using TestTask.Domain.Enums;

namespace TestTask.Infrastructure.Services.Directories
{
    public class ResourceService : GenericService<Resource, ResourceDTO>, IResourceService
    {
        public ResourceService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, unitOfWork.Resources, mapper)   
        {
        }

        public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            bool hasIncomingResources = (await _unitOfWork.IncomingResources
                .FindAsync(ir => ir.Resource.Id == id, cancellationToken))
                .Any();
            if (hasIncomingResources)
            {
                throw new InvalidOperationException("Нельзя удалить ресурс, так как есть связанные поступления.");
            }

            bool hasOutgoingResources = (await _unitOfWork.OutgoingResources
                .FindAsync(or => or.Resource.Id == id, cancellationToken))
                .Any();
            if (hasOutgoingResources)
            {
                throw new InvalidOperationException("Нельзя удалить ресурс, так как есть связанные отгрузки.");
            }

            bool hasBalances = (await _unitOfWork.Balances
                .FindAsync(b => b.Resource.Id == id, cancellationToken))
                .Any();
            if (hasBalances)
            {
                throw new InvalidOperationException("Нельзя удалить ресурс, так как есть связанные остатки на складе.");
            }

            await base.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task ChangeStatusAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var resource = await _repository.GetByIdAsync(id, cancellationToken);
            if (resource == null)
                throw new InvalidOperationException("Ресурс не найден.");

            resource.State = resource.State == DirectoriesStateEnum.Used ? DirectoriesStateEnum.Archived : DirectoriesStateEnum.Used;

            await _repository.UpdateAsync(resource, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task<IEnumerable<ResourceDTO>> GetByStateAsync(DirectoriesStateEnum state, CancellationToken cancellationToken = default)
        {
            var resource = await _unitOfWork.Resources.GetByStateAsync(state, cancellationToken);
            return _mapper.Map<IEnumerable<ResourceDTO>>(resource);
        }
    }
}

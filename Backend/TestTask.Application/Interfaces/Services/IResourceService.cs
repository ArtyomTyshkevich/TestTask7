using TestTask.Application.DTOs.Directories;
using TestTask.Domain.Entities.Directories;
using TestTask.Domain.Enums;

namespace TestTask.Application.Interfaces.Services
{
    public interface IResourceService : IGenericService<Resource, ResourceDTO>
    {
        Task ChangeStatusAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ResourceDTO>> GetByStateAsync(DirectoriesStateEnum state, CancellationToken cancellationToken = default);
    }
}

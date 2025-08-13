using TestTask.Domain.Entities.Directories;
using TestTask.Domain.Enums;

namespace TestTask.Application.Interfaces.Repositories
{
    public interface IResourceRepository : IGenericRepository<Resource>
    {
        Task<IEnumerable<Resource>> GetByStateAsync(DirectoriesStateEnum state, CancellationToken cancellationToken);
    }
}

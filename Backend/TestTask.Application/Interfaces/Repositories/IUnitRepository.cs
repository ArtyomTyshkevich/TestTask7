using TestTask.Domain.Entities.Directories;
using TestTask.Domain.Enums;

namespace TestTask.Application.Interfaces.Repositories
{
    public interface IUnitRepository : IGenericRepository<Unit>
    {
        Task<IEnumerable<Unit>> GetByStateAsync(DirectoriesStateEnum state, CancellationToken cancellationToken);
    }
}

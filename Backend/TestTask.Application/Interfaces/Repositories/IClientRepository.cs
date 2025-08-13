using System.Threading.Tasks;
using TestTask.Domain.Entities.Directories;
using TestTask.Domain.Enums;

namespace TestTask.Application.Interfaces.Repositories
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<IEnumerable<Client>> GetByStateAsync(DirectoriesStateEnum state, CancellationToken cancellationToken);
    }
}

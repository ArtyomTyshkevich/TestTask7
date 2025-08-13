using TestTask.Application.DTOs.Directories;
using TestTask.Domain.Entities.Directories;
using TestTask.Domain.Enums;

namespace TestTask.Application.Interfaces.Services
{
    public interface IClientService : IGenericService<Client, ClientDTO>
    {
        Task ChangeStatusAsync(Guid id, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ClientDTO>> GetByStateAsync(DirectoriesStateEnum state, CancellationToken cancellationToken = default);
    }
}

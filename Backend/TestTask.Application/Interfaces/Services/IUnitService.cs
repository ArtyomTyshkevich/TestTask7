using TestTask.Application.DTOs.Directories;
using TestTask.Domain.Entities.Directories;
using TestTask.Domain.Enums;

namespace TestTask.Application.Interfaces.Services
{
    public interface IUnitService : IGenericService<Unit, UnitDTO>
    {
        Task<IEnumerable<UnitDTO>> GetByStateAsync(DirectoriesStateEnum state, CancellationToken cancellationToken = default);
        Task ChangeStatusAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

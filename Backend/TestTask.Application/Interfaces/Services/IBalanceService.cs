using TestTask.Application.DTOs.Warehouse;
using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Application.Interfaces.Services
{
    public interface IBalanceService : IGenericService<Balance, BalanceDTO>
    {
        Task<IEnumerable<BalanceDTO>> GetWithFiltersAsync(List<Guid> resourcesId, List<Guid> unitsId, CancellationToken cancellationToken = default);
        Task<FiltersData> GetFiltersDataAsync(CancellationToken cancellationToken = default);
    }
}
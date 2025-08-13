using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Application.Interfaces.Repositories
{
    public interface IBalanceRepository : IGenericRepository<Balance>
    {
        Task<IEnumerable<Balance>> GetWithFiltersAsync(List<Guid> resourcesId, List<Guid> unitsId, CancellationToken cancellationToken = default);
        Task<Balance?> GetByResourceAndUnitAsync(Guid resourceId, Guid unitId, CancellationToken cancellationToken = default);
    }
}

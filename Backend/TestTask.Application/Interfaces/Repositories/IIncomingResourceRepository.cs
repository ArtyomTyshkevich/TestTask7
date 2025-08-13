using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Application.Interfaces.Repositories
{
    public interface IIncomingResourceRepository : IGenericRepository<IncomingResource>
    {
        Task RemoveRangeAsync(IEnumerable<IncomingResource> entities, CancellationToken cancellationToken = default);
        Task<IncomingResource?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

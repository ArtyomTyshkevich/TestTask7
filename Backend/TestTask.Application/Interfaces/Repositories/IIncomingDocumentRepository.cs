using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Application.Interfaces.Repositories
{
    public interface IIncomingDocumentRepository : IGenericRepository<IncomingDocument>
    {
        Task<List<IncomingDocument>> GetFilteredAsync(
            List<string>? numbers,
            List<Guid>? resourceIds,
            List<Guid>? unitIds,
            DateTime? startDate,
            DateTime? endDate,
            CancellationToken cancellationToken);
        Task<IncomingDocument?> GetByIdWithResourcesAsync(Guid id, CancellationToken cancellationToken);
    }
}

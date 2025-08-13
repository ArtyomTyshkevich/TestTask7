using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Application.Interfaces.Repositories
{
    public interface IOutgoingDocumentRepository : IGenericRepository<OutgoingDocument>
    {
        Task<List<OutgoingDocument>> GetFilteredAsync(
          List<string>? numbers,
          List<Guid>? resourceIds,
          List<Guid>? unitIds,
          List<Guid>? clientIds,
          DateTime? startDate,
          DateTime? endDate,
          CancellationToken cancellationToken);
        Task<OutgoingDocument?> GetByIdWithResourcesAsync(Guid id, CancellationToken cancellationToken);
    }
}

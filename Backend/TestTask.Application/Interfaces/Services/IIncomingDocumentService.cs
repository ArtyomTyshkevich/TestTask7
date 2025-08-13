using TestTask.Application.DTOs.Warehouse;
using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Application.Interfaces.Services
{
    public interface IIncomingDocumentService : IGenericService<IncomingDocument, IncomingDocumentDTO>
    {
        Task<List<IncomingDocumentDTO>> GetFilteredAsync(
            List<string>? numbers,
            List<Guid>? resourceIds,
            List<Guid>? unitIds,
            DateTime? startDate,
        DateTime? endDate,
            CancellationToken cancellationToken);
        Task<FiltersData> GetFiltersDataAsync(CancellationToken cancellationToken = default);
        Task AddAsync(IncomingDocumentDTO dto, CancellationToken cancellationToken = default);
    }
}

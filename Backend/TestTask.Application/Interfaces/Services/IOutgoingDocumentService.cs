using TestTask.Application.DTOs.Warehouse;
using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Application.Interfaces.Services
{
    public interface IOutgoingDocumentService : IGenericService<OutgoingDocument, OutgoingDocumentDTO>
    {
        Task<List<OutgoingDocumentDTO>> GetFilteredAsync(
          List<string>? numbers,
          List<Guid>? resourceIds,
          List<Guid>? unitIds,
          List<Guid>? clientIds,
          DateTime? startDate,
      DateTime? endDate,
          CancellationToken cancellationToken);
        Task<FiltersData> GetFiltersDataAsync(CancellationToken cancellationToken = default);
        Task RevokeAsync(Guid outgoingDocumentId, CancellationToken cancellationToken = default);
        Task SignAsync(Guid outgoingDocumentId, CancellationToken cancellationToken = default);
        Task<OutgoingDocumentDTO> CreateAndSignAsync(OutgoingDocumentDTO dto, CancellationToken cancellationToken = default);
        Task<OutgoingDocumentDTO> UpdateAndSignAsync(OutgoingDocumentDTO dto, CancellationToken cancellationToken = default);
    }
}

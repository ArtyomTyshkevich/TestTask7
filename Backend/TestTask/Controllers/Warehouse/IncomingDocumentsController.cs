using Microsoft.AspNetCore.Mvc;
using TestTask.API.Controllers;
using TestTask.Application.DTOs.Warehouse;
using TestTask.Application.Interfaces.Services;
using TestTask.Domain.Entities.Warehouse;

[Route("api/[controller]")]
[ApiController]
public class IncomingDocumentsController : GenericController<IncomingDocument, IncomingDocumentDTO, IIncomingDocumentService>
{
    public IncomingDocumentsController(IIncomingDocumentService service) : base(service)
    {
    }

    [HttpGet("filtered")]
    public async Task<IActionResult> GetFilteredAsync(
       [FromQuery] List<string>? numbers,
       [FromQuery] List<Guid>? resourceIds,
       [FromQuery] List<Guid>? unitIds,
       [FromQuery] DateTime? startDate,
       [FromQuery] DateTime? endDate,
       CancellationToken cancellationToken)
        {
        var result = await _service.GetFilteredAsync(
            numbers,
            resourceIds,
            unitIds,
            startDate,
            endDate,
            cancellationToken);

        return Ok(result);
    }
    [HttpGet("filters")]
    public async Task<IActionResult> GetFilters(CancellationToken cancellationToken)
    {
        var filters = await _service.GetFiltersDataAsync(cancellationToken);
        return Ok(filters);
    }
}

using Microsoft.AspNetCore.Mvc;
using TestTask.API.Controllers;
using TestTask.Application.DTOs.Warehouse;
using TestTask.Application.Interfaces.Services;
using TestTask.Domain.Entities.Warehouse;

[Route("api/[controller]")]
[ApiController]
public class OutgoingDocumentsController : GenericController<OutgoingDocument, OutgoingDocumentDTO, IOutgoingDocumentService>
{
    public OutgoingDocumentsController(IOutgoingDocumentService service) : base(service)
    {
    }

    [HttpGet("filtered")]
    public async Task<IActionResult> GetFilteredAsync(
       [FromQuery] List<string>? numbers,
       [FromQuery] List<Guid>? resourceIds,
       [FromQuery] List<Guid>? unitIds,
       [FromQuery] List<Guid>? clientIds,
       [FromQuery] DateTime? startDate,
       [FromQuery] DateTime? endDate,
       CancellationToken cancellationToken)
    {
        var result = await _service.GetFilteredAsync(
            numbers,
            resourceIds,
            unitIds,
            clientIds,
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

    [HttpPost("{id:guid}/sign")]
    public async Task<IActionResult> Sign(Guid id, CancellationToken cancellationToken)
    {
        await _service.SignAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/revoke")]
    public async Task<IActionResult> Revoke(Guid id, CancellationToken cancellationToken)
    {
        await _service.RevokeAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpPost("create-and-sign")]
    public async Task<IActionResult> CreateAndSign([FromBody] OutgoingDocumentDTO dto, CancellationToken cancellationToken)
    {
        var created = await _service.CreateAndSignAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPost("update-and-sign")]
    public async Task<IActionResult> UpdateAndSign([FromBody] OutgoingDocumentDTO dto, CancellationToken cancellationToken)
    {
        var created = await _service.UpdateAndSignAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}

using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Interfaces.Services;

[Route("api/[controller]")]
[ApiController]
public class BalancesController : ControllerBase
{
    private readonly IBalanceService _service;

    public BalancesController(IBalanceService service)
    {
        _service = service;
    }

    [HttpGet("filtered")]
    public async Task<IActionResult> GetBalances(
        [FromQuery] List<Guid> resourcesId,
        [FromQuery] List<Guid> unitsId,
        CancellationToken cancellationToken)
    {
        var balances = await _service.GetWithFiltersAsync(resourcesId, unitsId, cancellationToken);
        return Ok(balances);
    }

    [HttpGet("filters")]
    public async Task<IActionResult> GetFilters(CancellationToken cancellationToken)
    {
        var filters = await _service.GetFiltersDataAsync(cancellationToken);
        return Ok(filters);
    }
}

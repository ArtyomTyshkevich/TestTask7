using Microsoft.AspNetCore.Mvc;
using TestTask.API.Controllers;
using TestTask.Application.DTOs.Directories;
using TestTask.Application.Interfaces.Services;
using TestTask.Domain.Entities.Directories;
using TestTask.Domain.Enums;

namespace TestTask.Api.Controllers.Directories
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : GenericController<Unit, UnitDTO, IUnitService>
    {
        public UnitsController(IUnitService service) : base(service)
        {
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> ChangeStatus(Guid id, CancellationToken cancellationToken)
        {
            await _service.ChangeStatusAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet("state/{state}")]
        public async Task<ActionResult<IEnumerable<UnitDTO>>> GetByState(DirectoriesStateEnum state, CancellationToken cancellationToken)
        {
            var units = await _service.GetByStateAsync(state, cancellationToken);
            return Ok(units);
        }
    }
}

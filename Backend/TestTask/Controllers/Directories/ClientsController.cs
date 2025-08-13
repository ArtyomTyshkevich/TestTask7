using Microsoft.AspNetCore.Mvc;
using TestTask.Application.DTOs.Directories;
using TestTask.Application.Interfaces.Services;
using TestTask.Domain.Entities.Directories;
using TestTask.Domain.Enums;

namespace TestTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : GenericController<Client, ClientDTO, IClientService>
    {
        public ClientsController(IClientService service) : base(service)
        {
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> ChangeStatus(Guid id, CancellationToken cancellationToken)
        {
            await _service.ChangeStatusAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet("state/{state}")]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetByState(DirectoriesStateEnum state, CancellationToken cancellationToken)
        {
            var clients = await _service.GetByStateAsync(state, cancellationToken);
            return Ok(clients);
        }
    }
}

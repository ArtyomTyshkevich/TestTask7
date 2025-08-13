using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Interfaces.Services;

namespace TestTask.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<TEntity, TDto, TService> : ControllerBase
        where TEntity : class
        where TDto : class
        where TService : IGenericService<TEntity, TDto>
    {
        protected readonly TService _service;

        public GenericController(TService service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TDto>> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Create(TDto dto)
        {
            await _service.AddAsync(dto);
            return Ok();
        }

        [HttpPut()]
        public virtual async Task<ActionResult> Update(TDto dto)
        {
            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id); 
            return NoContent();
        }
    }
}

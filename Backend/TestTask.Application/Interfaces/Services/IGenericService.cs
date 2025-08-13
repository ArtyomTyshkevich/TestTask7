using System.Linq.Expressions;

namespace TestTask.Application.Interfaces.Services
{
    public interface IGenericService<TEntity, TDto>
        where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(TDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(TDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

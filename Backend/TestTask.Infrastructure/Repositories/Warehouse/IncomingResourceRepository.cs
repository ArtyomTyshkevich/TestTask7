using Microsoft.EntityFrameworkCore;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Domain.Entities.Warehouse;
using TestTask.Infrastructure.Contexts;

namespace TestTask.Infrastructure.Repositories.Warehouse
{
    public class IncomingResourceRepository : GenericRepository<IncomingResource>, IIncomingResourceRepository
    {
        public IncomingResourceRepository(TestTaskDbContext context) : base(context)
        {
        }

        public async Task RemoveRangeAsync(IEnumerable<IncomingResource> entities, CancellationToken cancellationToken = default)
        {
            _context.IncomingResources.RemoveRange(entities);
        }

        public async Task<IncomingResource?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.IncomingResources
                .AsNoTracking()
                .Include(r => r.Resource)
                .Include(r => r.Unit)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

    }
}

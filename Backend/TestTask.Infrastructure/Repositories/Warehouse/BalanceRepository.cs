using Microsoft.EntityFrameworkCore;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Domain.Entities.Warehouse;
using TestTask.Infrastructure.Contexts;

namespace TestTask.Infrastructure.Repositories.Warehouse
{
    public class BalanceRepository : GenericRepository<Balance>, IBalanceRepository
    {
        public BalanceRepository(TestTaskDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Balance>> GetWithFiltersAsync(List<Guid> resourcesId, List<Guid> unitsId, CancellationToken cancellationToken = default)
        {
            IQueryable<Balance> filteredBalances = _dbSet;

            if (resourcesId != null && resourcesId.Any())
            {
                filteredBalances = filteredBalances.Where(b => resourcesId.Contains(b.Resource.Id));
            }

            if (unitsId != null && unitsId.Any())
            {
                filteredBalances = filteredBalances.Where(b => unitsId.Contains(b.Unit.Id));
            }

            return await filteredBalances.ToListAsync(cancellationToken);
        }
        public async Task<Balance?> GetByResourceAndUnitAsync(Guid resourceId, Guid unitId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(
                    b => b.Resource.Id == resourceId && b.Unit.Id == unitId,
                    cancellationToken
                );
        }

    }
}

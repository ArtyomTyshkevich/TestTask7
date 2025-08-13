using Microsoft.EntityFrameworkCore;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Domain.Entities.Directories;
using TestTask.Domain.Enums;
using TestTask.Infrastructure.Contexts;

namespace TestTask.Infrastructure.Repositories.Directories
{
    public class UnitRepository : GenericRepository<Unit>, IUnitRepository
    {
        public UnitRepository(TestTaskDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Unit>> GetByStateAsync(DirectoriesStateEnum state, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Where(unit=>unit.State== state)
                .ToListAsync(cancellationToken);
        }
    }
}

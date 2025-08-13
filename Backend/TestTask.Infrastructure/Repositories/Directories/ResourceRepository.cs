using Microsoft.EntityFrameworkCore;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Domain.Entities.Directories;
using TestTask.Domain.Enums;
using TestTask.Infrastructure.Contexts;

namespace TestTask.Infrastructure.Repositories.Directories
{
    public class ResourceRepository : GenericRepository<Resource>, IResourceRepository
    {
        public ResourceRepository(TestTaskDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Resource>> GetByStateAsync(DirectoriesStateEnum state, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Where(resource => resource.State == state)
                .ToListAsync(cancellationToken);

        }
    }
}

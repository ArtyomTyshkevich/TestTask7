using Microsoft.EntityFrameworkCore;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Domain.Entities.Directories;
using TestTask.Domain.Enums;
using TestTask.Infrastructure.Contexts;

namespace TestTask.Infrastructure.Repositories.Directories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(TestTaskDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Client>> GetByStateAsync(DirectoriesStateEnum state, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Where(client => client.State == state)
                .ToListAsync(cancellationToken);
        }
    }
}

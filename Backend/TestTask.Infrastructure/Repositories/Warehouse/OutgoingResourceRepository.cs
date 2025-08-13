using Microsoft.EntityFrameworkCore;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Domain.Entities.Warehouse;
using TestTask.Infrastructure.Contexts;

namespace TestTask.Infrastructure.Repositories.Warehouse
{
    public class OutgoingResourceRepository : GenericRepository<OutgoingResource>, IOutgoingResourceRepository
    {
        public OutgoingResourceRepository(TestTaskDbContext context) : base(context)
        {
        }
    }
}

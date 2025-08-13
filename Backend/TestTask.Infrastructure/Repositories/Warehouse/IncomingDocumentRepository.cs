using Microsoft.EntityFrameworkCore;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Domain.Entities.Warehouse;
using TestTask.Infrastructure.Contexts;

namespace TestTask.Infrastructure.Repositories.Warehouse
{
    public class IncomingDocumentRepository : GenericRepository<IncomingDocument>, IIncomingDocumentRepository
    {
        public IncomingDocumentRepository(TestTaskDbContext context) : base(context)
        {
        }

        public async Task<List<IncomingDocument>> GetFilteredAsync(
            List<string>? numbers,
            List<Guid>? resourceIds,
            List<Guid>? unitIds,
            DateTime? startDate,
            DateTime? endDate,
            CancellationToken cancellationToken)
        {
            var filteredIncomingDocuments = _context.IncomingDocuments.AsQueryable();

            if (numbers != null && numbers.Count > 0)
            {
                filteredIncomingDocuments = filteredIncomingDocuments.Where(d => numbers.Contains(d.Number));
            }

            if (resourceIds != null && resourceIds.Count > 0)
            {
                filteredIncomingDocuments = filteredIncomingDocuments.Where(d => d.IncomingResources.Any(r => resourceIds.Contains(r.Resource.Id)));
            }

            if (unitIds != null && unitIds.Count > 0)
            {
                filteredIncomingDocuments = filteredIncomingDocuments.Where(d => d.IncomingResources.Any(r => unitIds.Contains(r.Unit.Id)));
            }

            if (startDate.HasValue)
            {
                filteredIncomingDocuments = filteredIncomingDocuments.Where(d => d.Date >= startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                filteredIncomingDocuments = filteredIncomingDocuments.Where(d => d.Date <= endDate.Value.Date);
            }

            return await filteredIncomingDocuments.ToListAsync(cancellationToken);
        }
        public async Task<IncomingDocument?> GetByIdWithResourcesAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.IncomingDocuments
               .Include(d => d.IncomingResources)
                   .ThenInclude(r => r.Resource)
               .Include(d => d.IncomingResources)
                   .ThenInclude(r => r.Unit)
                   .AsNoTracking()
               .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        }
        public override async Task AddAsync(IncomingDocument entity, CancellationToken cancellationToken = default)
        {
            foreach (var incomingResource in entity.IncomingResources)
            {
                if (incomingResource.Resource != null)
                {
                    _context.Attach(incomingResource.Resource);
                }

                if (incomingResource.Unit != null)
                {
                    _context.Attach(incomingResource.Unit);
                }
            }

            await base.AddAsync(entity, cancellationToken);
        }
    }
}


using Microsoft.EntityFrameworkCore;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Domain.Entities.Warehouse;
using TestTask.Infrastructure.Contexts;

namespace TestTask.Infrastructure.Repositories.Warehouse
{
    public class OutgoingDocumentRepository : GenericRepository<OutgoingDocument>, IOutgoingDocumentRepository
    {
        public OutgoingDocumentRepository(TestTaskDbContext context) : base(context)
        {
        }

        public async Task<List<OutgoingDocument>> GetFilteredAsync(
           List<string>? numbers,
           List<Guid>? resourceIds,
           List<Guid>? unitIds,
           List<Guid>? clientIds,
           DateTime? startDate,
           DateTime? endDate,
           CancellationToken cancellationToken)
        {
            var query = _context.OutgoingDocuments.AsQueryable();

            if (numbers != null && numbers.Count > 0)
            {
                query = query.Where(d => numbers.Contains(d.Number));
            }

            if (clientIds != null && clientIds.Count > 0)
            {
                query = query.Where(d => clientIds.Contains(d.Client.Id));
            }

            if (resourceIds != null && resourceIds.Count > 0)
            {
                query = query.Where(d => d.OutgoingResources.Any(r => resourceIds.Contains(r.Resource.Id)));
            }

            if (unitIds != null && unitIds.Count > 0)
            {
                query = query.Where(d => d.OutgoingResources.Any(r => unitIds.Contains(r.Unit.Id)));
            }

            if (startDate.HasValue)
            {
                query = query.Where(d => d.Date >= startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                query = query.Where(d => d.Date <= endDate.Value.Date);
            }

            return await query.ToListAsync(cancellationToken);
        }
        public override async Task AddAsync(OutgoingDocument outgoingDocument, CancellationToken cancellationToken = default)
        {
            outgoingDocument.State = Domain.Enums.OutgoingStateEnum.unsigned;

            var hasClient = outgoingDocument.Client != null;

            if (hasClient)
            {
                _context.Attach(outgoingDocument.Client);
            }

            var noResources = outgoingDocument.OutgoingResources == null || outgoingDocument.OutgoingResources.Count == 0;
            if (noResources)
            {
                throw new InvalidOperationException("Необходимо указать хотя бы один ресурс.");
            }

            foreach (var outgoingResource in outgoingDocument.OutgoingResources)
            {
                var hasResource = outgoingResource.Resource != null;
                var hasUnit = outgoingResource.Unit != null;

                if (hasResource)
                {
                    _context.Attach(outgoingResource.Resource);
                }

                if (hasUnit)
                {
                    _context.Attach(outgoingResource.Unit);
                }
            }

            await base.AddAsync(outgoingDocument, cancellationToken);
        }
        public override async Task UpdateAsync(OutgoingDocument outgoingDocument, CancellationToken cancellationToken = default)
        {
            outgoingDocument.State = Domain.Enums.OutgoingStateEnum.unsigned;

            var hasClient = outgoingDocument.Client != null;

            if (hasClient)
            {
                _context.Attach(outgoingDocument.Client);
            }

            var noResources = outgoingDocument.OutgoingResources == null || outgoingDocument.OutgoingResources.Count == 0;
            if (noResources)
            {
                throw new InvalidOperationException("Необходимо указать хотя бы один ресурс.");
            }

            foreach (var outgoingResource in outgoingDocument.OutgoingResources)
            {
                var hasResource = outgoingResource.Resource != null;
                var hasUnit = outgoingResource.Unit != null;

                if (hasResource)
                {
                    _context.Attach(outgoingResource.Resource);
                }

                if (hasUnit)
                {
                    _context.Attach(outgoingResource.Unit);
                }
            }

            await base.UpdateAsync(outgoingDocument, cancellationToken);
        }
        public async Task<OutgoingDocument?> GetByIdWithResourcesAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.OutgoingDocuments
               .Include(d => d.OutgoingResources)
                   .ThenInclude(r => r.Resource)
               .Include(d => d.OutgoingResources)
                   .ThenInclude(r => r.Unit)
                   .AsNoTracking()
               .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        }
    }
}

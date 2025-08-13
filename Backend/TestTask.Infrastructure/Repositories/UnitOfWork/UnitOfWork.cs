using TestTask.Application.Interfaces.Repositories;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Infrastructure.Contexts;
using TestTask.Infrastructure.Repositories.Directories;
using TestTask.Infrastructure.Repositories.Warehouse;

namespace TestTask.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestTaskDbContext _dbContext;

        public UnitOfWork(TestTaskDbContext dbContext)
        {
            _dbContext = dbContext;

            Resources = new ResourceRepository(_dbContext);
            Units = new UnitRepository(_dbContext);
            Clients = new ClientRepository(_dbContext);
            Balances = new BalanceRepository(_dbContext);
            IncomingDocuments = new IncomingDocumentRepository(_dbContext);
            IncomingResources = new IncomingResourceRepository(_dbContext);
            OutgoingDocuments = new OutgoingDocumentRepository(_dbContext);
            OutgoingResources = new OutgoingResourceRepository(_dbContext);
        }

        public IResourceRepository Resources { get; private set; }
        public IUnitRepository Units { get; private set; }
        public IClientRepository Clients { get; private set; }
        public IBalanceRepository Balances { get; private set; }
        public IIncomingDocumentRepository IncomingDocuments { get; private set; }
        public IIncomingResourceRepository IncomingResources { get; private set; }
        public IOutgoingDocumentRepository OutgoingDocuments { get; private set; }
        public IOutgoingResourceRepository OutgoingResources { get; private set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

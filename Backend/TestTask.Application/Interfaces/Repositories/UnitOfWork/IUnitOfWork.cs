using TestTask.Application.Interfaces.Repositories;

namespace TestTask.Application.Interfaces.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IResourceRepository Resources { get; }
        IUnitRepository Units { get; }
        IClientRepository Clients { get; }
        IBalanceRepository Balances { get; }
        IIncomingDocumentRepository IncomingDocuments { get; }
        IIncomingResourceRepository IncomingResources { get; }
        IOutgoingDocumentRepository OutgoingDocuments { get; }
        IOutgoingResourceRepository OutgoingResources { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

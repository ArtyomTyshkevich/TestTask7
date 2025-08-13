using TestTask.Application.DTOs.Directories;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Application.Interfaces.Services;
using TestTask.Domain.Entities.Directories;
using TestTask.Domain.Enums;
using AutoMapper;
using TestTask.Application.Services;

namespace TestTask.Infrastructure.Services.Directories
{
    public class ClientService : GenericService<Client, ClientDTO>, IClientService
    {
        public ClientService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, unitOfWork.Clients, mapper)
        {
        }

        public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            bool hasOutgoingDocuments = (await _unitOfWork.OutgoingDocuments
               .FindAsync(doc => doc.Client.Id == id, cancellationToken))
               .Any();

            if (hasOutgoingDocuments)
                throw new InvalidOperationException("Нельзя удалить клиента, так как существуют связанные документы отгрузки.");

            await base.DeleteAsync(id, cancellationToken);
        }

        public async Task ChangeStatusAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var client = await _repository.GetByIdAsync(id, cancellationToken);
            if (client == null)
                throw new InvalidOperationException("Клиент не найден.");

            client.State = client.State == DirectoriesStateEnum.Used
                ? DirectoriesStateEnum.Archived
                : DirectoriesStateEnum.Used;

            await _repository.UpdateAsync(client, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<ClientDTO>> GetByStateAsync(DirectoriesStateEnum state, CancellationToken cancellationToken = default)
        {
            var clients = await _unitOfWork.Clients.GetByStateAsync(state, cancellationToken);
            return _mapper.Map<IEnumerable<ClientDTO>>(clients);
        }
    }
}

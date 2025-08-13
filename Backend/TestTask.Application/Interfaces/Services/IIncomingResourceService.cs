using TestTask.Application.DTOs.Warehouse;
using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Application.Interfaces.Services
{
    public interface IIncomingResourceService : IGenericService<IncomingResource, IncomingResourceDTO>
    {
    }
}

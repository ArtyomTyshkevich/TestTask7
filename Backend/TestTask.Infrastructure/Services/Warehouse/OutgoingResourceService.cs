using AutoMapper;
using TestTask.Application.DTOs.Warehouse;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Application.Interfaces.Services;
using TestTask.Application.Services;
using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Infrastructure.Services.Warehouse
{
    public class OutgoingResourceService : GenericService<OutgoingResource, OutgoingResourceDTO>, IOutgoingResourceService
    {
        public OutgoingResourceService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, unitOfWork.OutgoingResources, mapper)
        {
        }
    }
}

using AutoMapper;
using TestTask.Application.DTOs.Directories;
using TestTask.Application.DTOs.Warehouse;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Application.Interfaces.Services;
using TestTask.Application.Services;
using TestTask.Domain.Entities.Warehouse;
using TestTask.Domain.Enums;

namespace TestTask.Infrastructure.Services.Warehouse
{
    public class BalanceService : GenericService<Balance, BalanceDTO>, IBalanceService
    {
        public BalanceService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, unitOfWork.Balances, mapper)
        {
        }

        public async Task<IEnumerable<BalanceDTO>> GetWithFiltersAsync(List<Guid> resourcesId, List<Guid> unitsId, CancellationToken cancellationToken = default)
        {
            var balances = await _unitOfWork.Balances.GetWithFiltersAsync(resourcesId, unitsId, cancellationToken);
            return _mapper.Map<IEnumerable<BalanceDTO>>(balances);
        }
        public async Task<FiltersData> GetFiltersDataAsync(CancellationToken cancellationToken = default)
        {
            var units = await _unitOfWork.Units.GetByStateAsync(DirectoriesStateEnum.Used, cancellationToken);
            var resources = await _unitOfWork.Resources.GetByStateAsync(DirectoriesStateEnum.Used, cancellationToken);

            return new FiltersData
            {
                unitDTOs = _mapper.Map<List<UnitDTO>>(units),
                ResourceDTOs = _mapper.Map<List<ResourceDTO>>(resources)
            };
        }

    }
}

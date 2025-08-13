using AutoMapper;
using TestTask.Application.DTOs.Directories;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Application.Interfaces.Services;
using TestTask.Application.Services;
using TestTask.Domain.Entities.Directories;
using TestTask.Domain.Enums;

namespace TestTask.Infrastructure.Services.Directories
{
    public class UnitService : GenericService<Unit,UnitDTO>, IUnitService
    {
        public UnitService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, unitOfWork.Units, mapper)
        {
        }

        public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            bool hasIncomingResources = (await _unitOfWork.IncomingResources
                .FindAsync(ir => ir.Unit.Id == id, cancellationToken))
                .Any();

            if (hasIncomingResources)
            {
                throw new InvalidOperationException("Нельзя удалить единицу измерения, так как есть связанные поступления.");
            }

            //bool hasOutgoingResources = (await _unitOfWork.OutgoingResources
            //   .FindAsync(or => or.Unit.Id == id, cancellationToken))
            //   .Any();

            //if (hasOutgoingResources)
            //{
            //    throw new InvalidOperationException("Нельзя удалить единицу измерения, так как есть связанные отгрузки.");
            //}

            bool hasBalances = (await _unitOfWork.Balances
               .FindAsync(b => b.Unit.Id == id, cancellationToken))
               .Any();

            if (hasBalances)
            {
                throw new InvalidOperationException("Нельзя удалить единицу измерения, так как есть связанные остатки на складе.");
            }

            await base.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task ChangeStatusAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var unit = await _repository.GetByIdAsync(id, cancellationToken);
            if (unit == null)
                throw new InvalidOperationException("Единица измерения не найдена.");

            unit.State = unit.State == DirectoriesStateEnum.Used ? DirectoriesStateEnum.Archived : DirectoriesStateEnum.Used;

            await _repository.UpdateAsync(unit, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<UnitDTO>> GetByStateAsync(DirectoriesStateEnum state, CancellationToken cancellationToken = default)
        {
            var unit = await _unitOfWork.Units.GetByStateAsync(state, cancellationToken);
            return _mapper.Map<IEnumerable<UnitDTO>>(unit);
        }
    }
}

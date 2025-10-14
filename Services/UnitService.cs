using AutoMapper;
using Entities.DTOs.UnitDto;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class UnitService : IUnitService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public UnitService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<UnitDto> CreateUnitAsync(UnitDtoForInsertion unitDtoForInsertion)
        {
            var unit = _mapper.Map<Entities.Models.Unit>(unitDtoForInsertion);
            _manager.UnitRepository.CreateUnit(unit);
            await _manager.SaveAsync();
            return _mapper.Map<UnitDto>(unit);
        }

        public async Task<UnitDto> DeleteUnitAsync(int id, bool? trackChanges)
        {
            var unit = await _manager.UnitRepository.GetUnitByIdAsync(id, trackChanges);
            _manager.UnitRepository.DeleteUnit(unit);
            await _manager.SaveAsync();
            return _mapper.Map<UnitDto>(unit);
        }

        public async Task<IEnumerable<UnitDto>> GetAllUnitsAsync(string lang, bool? trackChanges)
        {
            var unit = await _manager.UnitRepository.GetAllUnitsAsync(lang, trackChanges);
            return _mapper.Map<IEnumerable<UnitDto>>(unit);
        }

        public async Task<UnitDto> GetUnitByIdAsync(int id, bool? trackChanges)
        {
            var unit = await _manager.UnitRepository.GetUnitByIdAsync(id, trackChanges);
            return _mapper.Map<UnitDto>(unit);
        }

        public async Task<UnitDto> UpdateUnitAsync(UnitDtoForUpdate unitDtoForUpdate)
        {
            var unit = await _manager.UnitRepository.GetUnitByIdAsync(unitDtoForUpdate.ID, false);
            _mapper.Map(unitDtoForUpdate, unit);
            _manager.UnitRepository.UpdateUnit(unit);
            await _manager.SaveAsync();
            return _mapper.Map<UnitDto>(unit);
        }
    }
}

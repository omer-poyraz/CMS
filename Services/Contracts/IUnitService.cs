using Entities.DTOs.UnitDto;

namespace Services.Contracts
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitDto>> GetAllUnitsAsync(string lang, bool? trackChanges);
        Task<UnitDto> GetUnitByIdAsync(int id, bool? trackChanges);
        Task<UnitDto> CreateUnitAsync(UnitDtoForInsertion unitDtoForInsertion);
        Task<UnitDto> UpdateUnitAsync(UnitDtoForUpdate unitDtoForUpdate);
        Task<UnitDto> DeleteUnitAsync(int id, bool? trackChanges);
    }
}

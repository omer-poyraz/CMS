using Entities.Models;

namespace Repositories.Contracts
{
    public interface IUnitRepository : IRepositoryBase<Unit>
    {
        Task<IEnumerable<Unit>> GetAllUnitsAsync(string lang, bool? trackChanges);
        Task<Unit> GetUnitByIdAsync(int id, bool? trackChanges);
        Unit CreateUnit(Unit unit);
        Unit UpdateUnit(Unit unit);
        Unit DeleteUnit(Unit unit);
    }
}

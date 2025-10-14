using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class UnitRepository : RepositoryBase<Unit>, IUnitRepository
    {
        public UnitRepository(RepositoryContext context) : base(context) { }

        public Unit CreateUnit(Unit unit)
        {
            Create(unit);
            return unit;
        }

        public Unit DeleteUnit(Unit unit)
        {
            Delete(unit);
            return unit;
        }

        public async Task<IEnumerable<Unit>> GetAllUnitsAsync(string lang, bool? trackChanges)
        {
            var units = await FindAll(trackChanges).OrderBy(s => s.ID).ToListAsync();
            var dbContext = (RepositoryContext)base._context;
            foreach (var unit in units)
            {
                if (unit.Title is not null)
                {
                    try
                    {
                        var root = unit.Title.RootElement;
                        if (root.ValueKind == System.Text.Json.JsonValueKind.Array)
                        {
                            var idList = new List<int>();
                            foreach (var el in root.EnumerateArray())
                            {
                                if (el.ValueKind == System.Text.Json.JsonValueKind.Number && el.TryGetInt32(out int id))
                                    idList.Add(id);
                            }
                            if (idList.Count > 0)
                            {
                                var contents = dbContext.Contents
                                    .Where(c => idList.Contains(c.ID))
                                    .Select(c => new
                                    {
                                        code = c.Code,
                                        value = c.Value,
                                        type = c.Type
                                    })
                                    .ToList();
                                var langLower = lang?.ToLowerInvariant();
                                var match = contents.FirstOrDefault(c => c.code?.ToLowerInvariant() == langLower);
                                if (match != null)
                                {
                                    var json = System.Text.Json.JsonSerializer.Serialize(match);
                                    unit.Title = System.Text.Json.JsonDocument.Parse(json);
                                }
                                else
                                {
                                    unit.Title = System.Text.Json.JsonDocument.Parse("null");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error parsing Title JSON", ex);
                    }
                }
            }
            return units;
        }

        public async Task<Unit> GetUnitByIdAsync(int id, bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(id), trackChanges).SingleOrDefaultAsync();

        public Unit UpdateUnit(Unit unit)
        {
            Update(unit);
            return unit;
        }
    }
}

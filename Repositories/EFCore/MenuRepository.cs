using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(RepositoryContext context) : base(context) { }

        public Menu CreateMenu(Menu menu)
        {
            Create(menu);
            return menu;
        }

        public Menu DeleteMenu(Menu menu)
        {
            Delete(menu);
            return menu;
        }

        public async Task<IEnumerable<Menu>> GetAllMenusAsync(string lang, bool? trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(s => s.ID)
                .Include(s => s.Translations.Where(t => t.Lang == lang))
                .ToListAsync();

        public async Task<IEnumerable<Menu>> GetAllMenusByGroupAsync(int id, string lang, bool? trackChanges) =>
            await FindAll(trackChanges)
                .Where(s => s.MenuGroupID != null && s.MenuGroupID == id)
                .OrderBy(s => s.ID)
                .Include(s => s.Translations.Where(t => t.Lang == lang))
                .ToListAsync();

        public async Task<Menu> GetMenuByIdAsync(int id, bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(id), trackChanges)
                .Include(s => s.Translations)
                .SingleOrDefaultAsync();

        public async Task<Menu> GetMenuBySortAsync(int sort, bool? trackChanges) =>
            await FindByCondition(s => s.Sort == sort, trackChanges)
                .Include(s => s.Translations)
                .SingleOrDefaultAsync();

        public Menu SortMenu(Menu menu)
        {
            Update(menu);
            return menu;
        }

        public Menu UpdateMenu(Menu menu)
        {
            Update(menu);
            return menu;
        }
    }
}

using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class MenuGroupRepository : RepositoryBase<MenuGroup>, IMenuGroupRepository
    {
        public MenuGroupRepository(RepositoryContext context) : base(context) { }

        public MenuGroup CreateMenuGroup(MenuGroup menuGroup)
        {
            Create(menuGroup);
            return menuGroup;
        }

        public MenuGroup DeleteMenuGroup(MenuGroup menuGroup)
        {
            Delete(menuGroup);
            return menuGroup;
        }

        public async Task<IEnumerable<MenuGroup>> GetAllMenuGroupsAsync(string lang, bool? trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(s => s.ID)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))
                .Include(s => s.Menus)
                .ToListAsync();

        public async Task<MenuGroup?> GetMenuGroupByIdAsync(int id, string lang, bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(id), trackChanges)
                .Include(s => s.Translations)
                .Include(s => s.Menus)
                .SingleOrDefaultAsync();

        public MenuGroup UpdateMenuGroup(MenuGroup menuGroup)
        {
            Update(menuGroup);
            return menuGroup;
        }
    }
}

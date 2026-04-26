using Entities.Models;

namespace Repositories.Contracts
{
    public interface IMenuRepository : IRepositoryBase<Menu>
    {
        Task<IEnumerable<Menu>> GetAllMenusAsync(string lang, bool? trackChanges);
        Task<IEnumerable<Menu>> GetAllMenusByGroupAsync(int id, string lang, bool? trackChanges);
        Task<Menu> GetMenuByIdAsync(int id, bool? trackChanges);
        Task<Menu> GetMenuBySortAsync(int sort, bool? trackChanges);
        Menu CreateMenu(Menu menu);
        Menu SortMenu(Menu menu);
        Menu UpdateMenu(Menu menu);
        Menu DeleteMenu(Menu menu);
    }
}

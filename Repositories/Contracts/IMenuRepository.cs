using Entities.Models;

namespace Repositories.Contracts
{
    public interface IMenuRepository : IRepositoryBase<Menu>
    {
        Task<IEnumerable<Menu>> GetAllMenusAsync(bool? trackChanges);
        Task<IEnumerable<Menu>> GetAllMenusByGroupAsync(int id, bool? trackChanges);
        Task<Menu> GetMenuByIdAsync(int id, bool? trackChanges);
        Task<Menu> SortMenuAsync(int id, int sort, bool? trackChanges);
        Menu CreateMenu(Menu menu);
        Menu UpdateMenu(Menu menu);
        Menu DeleteMenu(Menu menu);
    }
}

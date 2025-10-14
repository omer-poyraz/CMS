using Entities.Models;

namespace Repositories.Contracts
{
    public interface IMenuGroupRepository : IRepositoryBase<MenuGroup>
    {
        Task<IEnumerable<MenuGroup>> GetAllMenuGroupsAsync(bool? trackChanges);
        Task<MenuGroup> GetMenuGroupByIdAsync(int id, bool? trackChanges);
        MenuGroup CreateMenuGroup(MenuGroup menuGroup);
        MenuGroup UpdateMenuGroup(MenuGroup menuGroup);
        MenuGroup DeleteMenuGroup(MenuGroup menuGroup);
    }
}

using Entities.Models;

namespace Repositories.Contracts
{
    public interface IMenuGroupRepository : IRepositoryBase<MenuGroup>
    {
        Task<IEnumerable<MenuGroup>> GetAllMenuGroupsAsync(string lang, bool? trackChanges);
        Task<MenuGroup> GetMenuGroupByIdAsync(int id, string lang, bool? trackChanges);
        MenuGroup CreateMenuGroup(MenuGroup menuGroup);
        MenuGroup UpdateMenuGroup(MenuGroup menuGroup);
        MenuGroup DeleteMenuGroup(MenuGroup menuGroup);
    }
}

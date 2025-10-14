using Entities.DTOs.MenuGroupDto;

namespace Services.Contracts
{
    public interface IMenuGroupService
    {
        Task<IEnumerable<MenuGroupDto>> GetAllMenuGroupsAsync(bool? trackChanges);
        Task<MenuGroupDto> GetMenuGroupByIdAsync(int id, bool? trackChanges);
        Task<MenuGroupDto> CreateMenuGroupAsync(MenuGroupDtoForInsertion menuGroupDtoForInsertion);
        Task<MenuGroupDto> UpdateMenuGroupAsync(MenuGroupDtoForUpdate menuGroupDtoForUpdate);
        Task<MenuGroupDto> DeleteMenuGroupAsync(int id, bool? trackChanges);
    }
}

using Entities.DTOs.MenuDto;

namespace Services.Contracts
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuDto>> GetAllMenusAsync(string lang, bool? trackChanges);
        Task<IEnumerable<MenuDto>> GetAllMenusByGroupAsync(int id, string lang, bool? trackChanges);
        Task<MenuDto> GetMenuByIdAsync(int id, bool? trackChanges);
        Task<MenuDto> CreateMenuAsync(MenuDtoForInsertion menuDtoForInsertion);
        Task<MenuDto> UpdateMenuAsync(MenuDtoForUpdate menuDtoForUpdate);
        Task<MenuDto> SortMenuAsync(int id, int sort, bool? trackChanges);
        Task<MenuDto> DeleteMenuAsync(int id, bool? trackChanges);
    }
}

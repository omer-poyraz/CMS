using AutoMapper;
using Entities.DTOs.MenuDto;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class MenuService : IMenuService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public MenuService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<MenuDto> CreateMenuAsync(MenuDtoForInsertion menuDtoForInsertion)
        {
            var menu = _mapper.Map<Entities.Models.Menu>(menuDtoForInsertion);
            _manager.MenuRepository.CreateMenu(menu);
            await _manager.SaveAsync();
            return _mapper.Map<MenuDto>(menu);
        }

        public async Task<MenuDto> DeleteMenuAsync(int id, bool? trackChanges)
        {
            var menu = await _manager.MenuRepository.GetMenuByIdAsync(id, trackChanges);
            _manager.MenuRepository.DeleteMenu(menu!);
            await _manager.SaveAsync();
            return _mapper.Map<MenuDto>(menu);
        }

        public async Task<IEnumerable<MenuDto>> GetAllMenusAsync(string lang, bool? trackChanges)
        {
            var menu = await _manager.MenuRepository.GetAllMenusAsync(lang, trackChanges);
            return _mapper.Map<IEnumerable<MenuDto>>(menu);
        }

        public async Task<IEnumerable<MenuDto>> GetAllMenusByGroupAsync(int id, string lang, bool? trackChanges)
        {
            var menu = await _manager.MenuRepository.GetAllMenusByGroupAsync(id, lang, trackChanges);
            return _mapper.Map<IEnumerable<MenuDto>>(menu);
        }

        public async Task<MenuDto> GetMenuByIdAsync(int id, bool? trackChanges)
        {
            var menu = await _manager.MenuRepository.GetMenuByIdAsync(id, trackChanges);
            return _mapper.Map<MenuDto>(menu);
        }

        public async Task<MenuDto> SortMenuAsync(int id, int sort, bool? trackChanges)
        {
            var menu = await _manager.MenuRepository.GetMenuByIdAsync(id, trackChanges);
            var sortMenu = await _manager.MenuRepository.GetMenuBySortAsync(sort, trackChanges);
            if (sortMenu != null)
            {
                sortMenu.Sort = menu!.Sort;
                _manager.MenuRepository.SortMenu(sortMenu);
            }
            menu!.Sort = sort;
            _manager.MenuRepository.SortMenu(menu);
            await _manager.SaveAsync();
            return _mapper.Map<MenuDto>(menu);
        }

        public async Task<MenuDto> UpdateMenuAsync(MenuDtoForUpdate menuDtoForUpdate)
        {
            var menu = await _manager.MenuRepository.GetMenuByIdAsync(menuDtoForUpdate.ID, false);
            _mapper.Map(menuDtoForUpdate, menu);
            _manager.MenuRepository.UpdateMenu(menu!);
            await _manager.SaveAsync();
            return _mapper.Map<MenuDto>(menu);
        }
    }
}

using AutoMapper;
using Entities.DTOs.MenuGroupDto;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class MenuGroupService : IMenuGroupService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public MenuGroupService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<MenuGroupDto> CreateMenuGroupAsync(MenuGroupDtoForInsertion menuGroupDtoForInsertion)
        {
            var menuGroup = _mapper.Map<Entities.Models.MenuGroup>(menuGroupDtoForInsertion);
            _manager.MenuGroupRepository.CreateMenuGroup(menuGroup);
            await _manager.SaveAsync();
            return _mapper.Map<MenuGroupDto>(menuGroup);
        }

        public async Task<MenuGroupDto> DeleteMenuGroupAsync(int id, bool? trackChanges)
        {
            var menuGroup = await _manager.MenuGroupRepository.GetMenuGroupByIdAsync(id, trackChanges);
            _manager.MenuGroupRepository.DeleteMenuGroup(menuGroup);
            await _manager.SaveAsync();
            return _mapper.Map<MenuGroupDto>(menuGroup);
        }

        public async Task<IEnumerable<MenuGroupDto>> GetAllMenuGroupsAsync(bool? trackChanges)
        {
            var menuGroup = await _manager.MenuGroupRepository.GetAllMenuGroupsAsync(trackChanges);
            return _mapper.Map<IEnumerable<MenuGroupDto>>(menuGroup);
        }

        public async Task<MenuGroupDto> GetMenuGroupByIdAsync(int id, bool? trackChanges)
        {
            var menuGroup = await _manager.MenuGroupRepository.GetMenuGroupByIdAsync(id, trackChanges);
            return _mapper.Map<MenuGroupDto>(menuGroup);
        }

        public async Task<MenuGroupDto> UpdateMenuGroupAsync(MenuGroupDtoForUpdate menuGroupDtoForUpdate)
        {
            var menuGroup = await _manager.MenuGroupRepository.GetMenuGroupByIdAsync(menuGroupDtoForUpdate.ID, false);
            _mapper.Map(menuGroupDtoForUpdate, menuGroup);
            _manager.MenuGroupRepository.UpdateMenuGroup(menuGroup);
            await _manager.SaveAsync();
            return _mapper.Map<MenuGroupDto>(menuGroup);
        }
    }
}

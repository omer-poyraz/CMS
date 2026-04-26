using AutoMapper;
using Entities.DTOs.SectionItemDto;
using Entities.RequestFeature;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class SectionItemService : ISectionItemService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public SectionItemService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<SectionItemDto> CreateSectionItemAsync(SectionItemDtoForInsertion sectionItemGroupDtoForInsertion)
        {
            try
            {
                var sectionItemGroup = _mapper.Map<Entities.Models.SectionItem>(sectionItemGroupDtoForInsertion);
                _manager.SectionItemRepository.CreateSectionItem(sectionItemGroup);
                await _manager.SaveAsync();
                return _mapper.Map<SectionItemDto>(sectionItemGroup);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<SectionItemDto> DeleteSectionItemAsync(int id, bool? trackChanges)
        {
            var sectionItemGroup = await _manager.SectionItemRepository.GetSectionItemByIdAsync(id, trackChanges);
            _manager.SectionItemRepository.DeleteSectionItem(sectionItemGroup);
            await _manager.SaveAsync();
            return _mapper.Map<SectionItemDto>(sectionItemGroup);
        }

        public async Task<IEnumerable<SectionItemDto>> GetAllSectionItemsAsync(bool? trackChanges)
        {
            var sectionItems = await _manager.SectionItemRepository.GetAllSectionItemsAsync(trackChanges);
            return _mapper.Map<IEnumerable<SectionItemDto>>(sectionItems);
        }

        public async Task<SectionItemDto> GetSectionItemByIdAsync(int id, bool? trackChanges)
        {
            var sectionItemGroup = await _manager.SectionItemRepository.GetSectionItemByIdAsync(id, trackChanges);
            return _mapper.Map<SectionItemDto>(sectionItemGroup);
        }

        public async Task<SectionItemDto> UpdateSectionItemAsync(SectionItemDtoForUpdate sectionItemDtoForUpdate)
        {
            var sectionItem = await _manager.SectionItemRepository.GetSectionItemByIdAsync(sectionItemDtoForUpdate.ID, false);
            _mapper.Map(sectionItemDtoForUpdate, sectionItem);
            _manager.SectionItemRepository.UpdateSectionItem(sectionItem);
            await _manager.SaveAsync();
            return _mapper.Map<SectionItemDto>(sectionItem);
        }
    }
}

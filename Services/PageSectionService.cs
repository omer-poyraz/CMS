using AutoMapper;
using Entities.DTOs.PageSectionDto;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class PageSectionService : IPageSectionService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public PageSectionService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<PageSectionDto> CreatePageSectionAsync(PageSectionDtoForInsertion pageSectionGroupDtoForInsertion)
        {
            try
            {
                var pageSectionGroup = _mapper.Map<Entities.Models.PageSection>(pageSectionGroupDtoForInsertion);
                _manager.PageSectionRepository.CreatePageSection(pageSectionGroup);
                await _manager.SaveAsync();
                return _mapper.Map<PageSectionDto>(pageSectionGroup);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<PageSectionDto> DeletePageSectionAsync(int id, bool? trackChanges)
        {
            var pageSectionGroup = await _manager.PageSectionRepository.GetPageSectionByIdAsync(id, trackChanges);
            _manager.PageSectionRepository.DeletePageSection(pageSectionGroup);
            await _manager.SaveAsync();
            return _mapper.Map<PageSectionDto>(pageSectionGroup);
        }

        public async Task<IEnumerable<PageSectionDto>> GetAllPageSectionsAsync(bool? trackChanges)
        {
            var pageSections = await _manager.PageSectionRepository.GetAllPageSectionsAsync(trackChanges);
            return _mapper.Map<IEnumerable<PageSectionDto>>(pageSections);
        }

        public async Task<PageSectionDto> GetPageSectionByIdAsync(int id, bool? trackChanges)
        {
            var pageSectionGroup = await _manager.PageSectionRepository.GetPageSectionByIdAsync(id, trackChanges);
            return _mapper.Map<PageSectionDto>(pageSectionGroup);
        }

        public async Task<PageSectionDto> UpdatePageSectionAsync(PageSectionDtoForUpdate pageSectionDtoForUpdate)
        {
            var pageSection = await _manager.PageSectionRepository.GetPageSectionByIdAsync(pageSectionDtoForUpdate.ID, false);
            _mapper.Map(pageSectionDtoForUpdate, pageSection);
            _manager.PageSectionRepository.UpdatePageSection(pageSection);
            await _manager.SaveAsync();
            return _mapper.Map<PageSectionDto>(pageSection);
        }
    }
}

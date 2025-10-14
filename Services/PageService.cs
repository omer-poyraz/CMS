using AutoMapper;
using Entities.DTOs.PageDto;
using Entities.RequestFeature;
using Entities.RequestFeature.Page;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class PageService : IPageService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public PageService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<PageDto> CreatePageAsync(PageDtoForInsertion pageGroupDtoForInsertion)
        {
            try
            {
                var pageGroup = _mapper.Map<Entities.Models.Page>(pageGroupDtoForInsertion);
                _manager.PageRepository.CreatePage(pageGroup);
                await _manager.SaveAsync();
                return _mapper.Map<PageDto>(pageGroup);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<PageDto> DeletePageAsync(int id, bool? trackChanges)
        {
            var pageGroup = await _manager.PageRepository.GetPageByIdAsync(id, trackChanges);
            _manager.PageRepository.DeletePage(pageGroup);
            await _manager.SaveAsync();
            return _mapper.Map<PageDto>(pageGroup);
        }

        public async Task<(IEnumerable<PageDto> pageDtos, MetaData metaData)> GetAllPagesAsync(PageParameters pageParameters, bool? trackChanges)
        {
            var pages = await _manager.PageRepository.GetAllPagesAsync(pageParameters, trackChanges);
            var pageDto = _mapper.Map<IEnumerable<PageDto>>(pages);
            return (pageDto, pages.MetaData);
        }

        public async Task<PageDto> GetPageByIdAsync(int id, bool? trackChanges)
        {
            var pageGroup = await _manager.PageRepository.GetPageByIdAsync(id, trackChanges);
            return _mapper.Map<PageDto>(pageGroup);
        }

        public async Task<PageDto> GetPageBySlugAsync(string slug, string lang, bool? trackChanges)
        {
            var pageGroup = await _manager.PageRepository.GetPageBySlugAsync(slug, lang, trackChanges);
            return _mapper.Map<PageDto>(pageGroup);
        }

        public async Task<PageDto> UpdatePageAsync(PageDtoForUpdate pageDtoForUpdate)
        {
            var page = await _manager.PageRepository.GetPageByIdAsync(pageDtoForUpdate.ID, false);
            _mapper.Map(pageDtoForUpdate, page);
            _manager.PageRepository.UpdatePage(page);
            await _manager.SaveAsync();
            return _mapper.Map<PageDto>(page);
        }
    }
}

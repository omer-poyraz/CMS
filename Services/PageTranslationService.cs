using AutoMapper;
using Entities.DTOs.PageTranslationDto;
using Entities.RequestFeature;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class PageTranslationService : IPageTranslationService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public PageTranslationService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<PageTranslationDto> CreatePageTranslationAsync(PageTranslationDtoForInsertion pageTranslationGroupDtoForInsertion)
        {
            try
            {
                var pageTranslationGroup = _mapper.Map<Entities.Models.PageTranslation>(pageTranslationGroupDtoForInsertion);
                _manager.PageTranslationRepository.CreatePageTranslation(pageTranslationGroup);
                await _manager.SaveAsync();
                return _mapper.Map<PageTranslationDto>(pageTranslationGroup);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<PageTranslationDto> DeletePageTranslationAsync(int id, bool? trackChanges)
        {
            var pageTranslationGroup = await _manager.PageTranslationRepository.GetPageTranslationByIdAsync(id, trackChanges);
            _manager.PageTranslationRepository.DeletePageTranslation(pageTranslationGroup);
            await _manager.SaveAsync();
            return _mapper.Map<PageTranslationDto>(pageTranslationGroup);
        }

        public async Task<IEnumerable<PageTranslationDto>> GetAllPageTranslationsAsync(bool? trackChanges)
        {
            var pageTranslations = await _manager.PageTranslationRepository.GetAllPageTranslationsAsync(trackChanges);
            return _mapper.Map<IEnumerable<PageTranslationDto>>(pageTranslations);
        }

        public async Task<PageTranslationDto> GetPageTranslationByIdAsync(int id, bool? trackChanges)
        {
            var pageTranslationGroup = await _manager.PageTranslationRepository.GetPageTranslationByIdAsync(id, trackChanges);
            return _mapper.Map<PageTranslationDto>(pageTranslationGroup);
        }

        public async Task<PageTranslationDto> UpdatePageTranslationAsync(PageTranslationDtoForUpdate pageTranslationDtoForUpdate)
        {
            var pageTranslation = await _manager.PageTranslationRepository.GetPageTranslationByIdAsync(pageTranslationDtoForUpdate.ID, false);
            _mapper.Map(pageTranslationDtoForUpdate, pageTranslation);
            _manager.PageTranslationRepository.UpdatePageTranslation(pageTranslation);
            await _manager.SaveAsync();
            return _mapper.Map<PageTranslationDto>(pageTranslation);
        }
    }
}

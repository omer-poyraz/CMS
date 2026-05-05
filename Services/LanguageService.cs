using AutoMapper;
using Entities.DTOs.LanguageDto;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public LanguageService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<LanguageDto> CreateLanguageAsync(LanguageDtoForInsertion languageDtoForInsertion)
        {
            var language = _mapper.Map<Entities.Models.Language>(languageDtoForInsertion);
            _manager.LanguageRepository.CreateLanguage(language);
            await _manager.SaveAsync();
            return _mapper.Map<LanguageDto>(language);
        }

        public async Task<LanguageDto> DeleteLanguageAsync(int id, bool? trackChanges)
        {
            var language = await _manager.LanguageRepository.GetLanguageByIdAsync(id, "tr", trackChanges);
            _manager.LanguageRepository.DeleteLanguage(language!);
            await _manager.SaveAsync();
            return _mapper.Map<LanguageDto>(language);
        }

        public async Task<IEnumerable<LanguageDto>> GetAllLanguagesAsync(string lang, bool? trackChanges)
        {
            var language = await _manager.LanguageRepository.GetAllLanguagesAsync(lang, trackChanges);
            return _mapper.Map<IEnumerable<LanguageDto>>(language);
        }

        public async Task<LanguageDto> GetLanguageByIdAsync(int id, string lang, bool? trackChanges)
        {
            var language = await _manager.LanguageRepository.GetLanguageByIdAsync(id, lang, trackChanges);
            return _mapper.Map<LanguageDto>(language);
        }

        public async Task<LanguageDto> UpdateLanguageAsync(LanguageDtoForUpdate languageDtoForUpdate)
        {
            var language = await _manager.LanguageRepository.GetLanguageByIdAsync(languageDtoForUpdate.ID, "tr", false);
            _mapper.Map(languageDtoForUpdate, language);
            _manager.LanguageRepository.UpdateLanguage(language!);
            await _manager.SaveAsync();
            return _mapper.Map<LanguageDto>(language);
        }
    }
}

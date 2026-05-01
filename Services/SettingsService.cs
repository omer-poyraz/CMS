using AutoMapper;
using Entities.DTOs.SettingsDto;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public SettingsService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<SettingsDto> CreateSettingsAsync(SettingsDtoForInsertion settingsGroupDtoForInsertion)
        {
            var settingsGroup = _mapper.Map<Settings>(settingsGroupDtoForInsertion);
            _manager.SettingsRepository.CreateSettings(settingsGroup);
            await _manager.SaveAsync();
            return _mapper.Map<SettingsDto>(settingsGroup);
        }

        public async Task<SettingsDto> DeleteSettingsAsync(int id, bool? trackChanges)
        {
            var settingsGroup = await _manager.SettingsRepository.GetSettingsByIdAsync(id, trackChanges);
            _manager.SettingsRepository.DeleteSettings(settingsGroup!);
            await _manager.SaveAsync();
            return _mapper.Map<SettingsDto>(settingsGroup);
        }

        public async Task<IEnumerable<SettingsDto>> GetAllSettingsAsync(string? lang, bool? trackChanges)
        {
            var settingsGroup = await _manager.SettingsRepository.GetAllSettingsAsync(lang, trackChanges);
            return _mapper.Map<IEnumerable<SettingsDto>>(settingsGroup);
        }

        public async Task<SettingsDto> GetSettingsByIdAsync(int id, bool? trackChanges)
        {
            var settingsGroup = await _manager.SettingsRepository.GetSettingsByIdAsync(id, trackChanges);
            return _mapper.Map<SettingsDto>(settingsGroup);
        }

        public async Task<SettingsDto> GetSettingsAsync(string? lang, bool? trackChanges)
        {
            var settingsGroup = await _manager.SettingsRepository.GetSettingsAsync(lang, trackChanges);
            return _mapper.Map<SettingsDto>(settingsGroup);
        }

        public async Task<SettingsDto> UpdateSettingsAsync(SettingsDtoForUpdate settingsGroupDtoForUpdate)
        {
            var settingsGroup = await _manager.SettingsRepository.GetSettingsAsync(null!, false);
            _mapper.Map(settingsGroupDtoForUpdate, settingsGroup);
            _manager.SettingsRepository.UpdateSettings(settingsGroup!);
            await _manager.SaveAsync();
            return _mapper.Map<SettingsDto>(settingsGroup);
        }
    }
}

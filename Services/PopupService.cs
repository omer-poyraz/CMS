using AutoMapper;
using Entities.DTOs.PopupDto;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class PopupService : IPopupService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public PopupService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<PopupDto> CreatePopupAsync(PopupDtoForInsertion popupDtoForInsertion)
        {
            var popup = _mapper.Map<Entities.Models.Popup>(popupDtoForInsertion);
            _manager.PopupRepository.CreatePopup(popup);
            await _manager.SaveAsync();
            return _mapper.Map<PopupDto>(popup);
        }

        public async Task<PopupDto> DeletePopupAsync(int id, bool? trackChanges)
        {
            var popup = await _manager.PopupRepository.GetPopupByIdAsync(id, trackChanges);
            _manager.PopupRepository.DeletePopup(popup!);
            await _manager.SaveAsync();
            return _mapper.Map<PopupDto>(popup);
        }

        public async Task<IEnumerable<PopupDto>> GetAllPopupsAsync(bool? trackChanges)
        {
            var popup = await _manager.PopupRepository.GetAllPopupsAsync(trackChanges);
            return _mapper.Map<IEnumerable<PopupDto>>(popup);
        }

        public async Task<PopupDto> GetPopupByIdAsync(int id, bool? trackChanges)
        {
            var popup = await _manager.PopupRepository.GetPopupByIdAsync(id, trackChanges);
            return _mapper.Map<PopupDto>(popup);
        }

        public async Task<PopupDto> UpdatePopupAsync(PopupDtoForUpdate popupDtoForUpdate)
        {
            var popup = await _manager.PopupRepository.GetPopupByIdAsync(popupDtoForUpdate.ID, false);
            _mapper.Map(popupDtoForUpdate, popup);
            _manager.PopupRepository.UpdatePopup(popup!);
            await _manager.SaveAsync();
            return _mapper.Map<PopupDto>(popup);
        }
    }
}

using Entities.DTOs.PopupDto;

namespace Services.Contracts
{
    public interface IPopupService
    {
        Task<IEnumerable<PopupDto>> GetAllPopupsAsync(bool? trackChanges);
        Task<PopupDto> GetPopupByIdAsync(int id, bool? trackChanges);
        Task<PopupDto> CreatePopupAsync(PopupDtoForInsertion popupDtoForInsertion);
        Task<PopupDto> UpdatePopupAsync(PopupDtoForUpdate popupDtoForUpdate);
        Task<PopupDto> DeletePopupAsync(int id, bool? trackChanges);
    }
}

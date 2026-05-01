using Entities.Models;

namespace Repositories.Contracts
{
    public interface IPopupRepository : IRepositoryBase<Popup>
    {
        Task<IEnumerable<Popup>> GetAllPopupsAsync(bool? trackChanges);
        Task<Popup?> GetPopupByIdAsync(int id, bool? trackChanges);
        Popup CreatePopup(Popup popup);
        Popup UpdatePopup(Popup popup);
        Popup DeletePopup(Popup popup);
    }
}

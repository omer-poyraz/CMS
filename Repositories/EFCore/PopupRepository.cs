using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class PopupRepository : RepositoryBase<Popup>, IPopupRepository
    {
        public PopupRepository(RepositoryContext context) : base(context) { }

        public Popup CreatePopup(Popup popup)
        {
            Create(popup);
            return popup;
        }

        public Popup DeletePopup(Popup popup)
        {
            Delete(popup);
            return popup;
        }

        public async Task<IEnumerable<Popup>> GetAllPopupsAsync(bool? trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(s => s.ID).ToListAsync();
        }

        public async Task<Popup> GetPopupByIdAsync(int id, bool? trackChanges)
        {
            return await FindByCondition(s => s.ID.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public Popup UpdatePopup(Popup popup)
        {
            Update(popup);
            return popup;
        }
    }
}

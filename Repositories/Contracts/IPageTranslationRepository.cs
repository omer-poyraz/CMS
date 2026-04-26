using Entities.Models;

namespace Repositories.Contracts
{
    public interface IPageTranslationRepository : IRepositoryBase<PageTranslation>
    {
        Task<IEnumerable<PageTranslation>> GetAllPageTranslationsAsync(bool? trackChanges);
        Task<PageTranslation> GetPageTranslationByIdAsync(int id, bool? trackChanges);
        PageTranslation CreatePageTranslation(PageTranslation pageTranslation);
        PageTranslation UpdatePageTranslation(PageTranslation pageTranslation);
        PageTranslation DeletePageTranslation(PageTranslation pageTranslation);
    }
}

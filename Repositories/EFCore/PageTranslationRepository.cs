using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class PageTranslationRepository : RepositoryBase<PageTranslation>, IPageTranslationRepository
    {

        public PageTranslationRepository(RepositoryContext context) : base(context) { }

        public PageTranslation CreatePageTranslation(PageTranslation pageTranslation)
        {
            Create(pageTranslation);
            return pageTranslation;
        }

        public PageTranslation DeletePageTranslation(PageTranslation pageTranslation)
        {
            Delete(pageTranslation);
            return pageTranslation;
        }

        public async Task<IEnumerable<PageTranslation>> GetAllPageTranslationsAsync(bool? trackChanges)
        {
            var pageTranslations = await FindAll(trackChanges)
                .OrderByDescending(s => s.ID)
                .ToListAsync();

            return pageTranslations;
        }

        public async Task<PageTranslation> GetPageTranslationByIdAsync(int id, bool? trackChanges)
        {
            return await FindByCondition(s => s.ID.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
        }

        public PageTranslation UpdatePageTranslation(PageTranslation pageTranslation)
        {
            Update(pageTranslation);
            return pageTranslation;
        }
    }
}

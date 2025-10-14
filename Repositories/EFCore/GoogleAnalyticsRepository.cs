using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class GoogleAnalyticsRepository : RepositoryBase<GoogleAnalytics>, IGoogleAnalyticsRepository
    {
        public GoogleAnalyticsRepository(RepositoryContext context) : base(context) { }

        public GoogleAnalytics CreateGoogleAnalytics(GoogleAnalytics googleAnalytics)
        {
            Create(googleAnalytics);
            return googleAnalytics;
        }

        public GoogleAnalytics DeleteGoogleAnalytics(GoogleAnalytics googleAnalytics)
        {
            Delete(googleAnalytics);
            return googleAnalytics;
        }

        public async Task<IEnumerable<GoogleAnalytics>> GetAllGoogleAnalyticsAsync(bool? trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(ga => ga.ID)
                .ToListAsync();
        }

        public async Task<GoogleAnalytics> GetGoogleAnalyticsByIdAsync(int id, bool? trackChanges)
        {
            return await FindByCondition(ga => ga.ID.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<GoogleAnalytics> GetActiveGoogleAnalyticsAsync(bool? trackChanges)
        {
            return await FindByCondition(ga => ga.Active.Equals(true), trackChanges).FirstOrDefaultAsync();
        }

        public GoogleAnalytics UpdateGoogleAnalytics(GoogleAnalytics googleAnalytics)
        {
            Update(googleAnalytics);
            return googleAnalytics;
        }
    }
}
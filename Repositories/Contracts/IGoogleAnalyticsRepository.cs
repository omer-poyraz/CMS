using Entities.Models;

namespace Repositories.Contracts
{
    public interface IGoogleAnalyticsRepository : IRepositoryBase<GoogleAnalytics>
    {
        Task<IEnumerable<GoogleAnalytics>> GetAllGoogleAnalyticsAsync(bool? trackChanges);
        Task<GoogleAnalytics> GetGoogleAnalyticsByIdAsync(int id, bool? trackChanges);
        Task<GoogleAnalytics> GetActiveGoogleAnalyticsAsync(bool? trackChanges);
        GoogleAnalytics CreateGoogleAnalytics(GoogleAnalytics googleAnalytics);
        GoogleAnalytics UpdateGoogleAnalytics(GoogleAnalytics googleAnalytics);
        GoogleAnalytics DeleteGoogleAnalytics(GoogleAnalytics googleAnalytics);
    }
}
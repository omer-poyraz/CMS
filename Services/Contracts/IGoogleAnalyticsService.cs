using Entities.DTOs.GoogleAnalyticsDto;

namespace Services.Contracts
{
    public interface IGoogleAnalyticsService
    {
        Task<AnalyticsReportResponse> GetAnalyticsReportGA4Async(AnalyticsReportRequest request);
        Task<AnalyticsSummary> GetAnalyticsSummaryAsync(DateTime? startDate = null, DateTime? endDate = null);
    }
}
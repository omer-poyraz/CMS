using System.Text.Json.Serialization;

namespace Entities.DTOs.GoogleAnalyticsDto
{
    public class AnalyticsReportResponse
    {
        public List<string> DimensionHeaders { get; set; } = new List<string>();
        public List<string> MetricHeaders { get; set; } = new List<string>();
        public List<ReportRow> Rows { get; set; } = new List<ReportRow>();
        public int RowCount { get; set; }
        public string? NextPageToken { get; set; }
    }

    public class ReportRow
    {
        public List<string> DimensionValues { get; set; } = new List<string>();
        public List<MetricValue> MetricValues { get; set; } = new List<MetricValue>();
    }

    public class MetricValue
    {
        public string? Value { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? NumericValue => double.TryParse(Value, out var result) ? result : null;
    }

    public class AnalyticsSummary
    {
        public int TotalUsers { get; set; }
        public int PageViews { get; set; }
        public int Sessions { get; set; }
        public double AvgSessionDuration { get; set; }
        public double BounceRate { get; set; }
        public List<TopPageItem> TopPages { get; set; } = new List<TopPageItem>();
        public List<TopReferrerItem> TopReferrers { get; set; } = new List<TopReferrerItem>();
        public List<UserLocationItem> UserLocations { get; set; } = new List<UserLocationItem>();
    }

    public class TopPageItem
    {
        public string? PagePath { get; set; }
        public string? PageTitle { get; set; }
        public int Views { get; set; }
    }

    public class TopReferrerItem
    {
        public string? Source { get; set; }
        public int Users { get; set; }
    }

    public class UserLocationItem
    {
        public string? Country { get; set; }
        public int Users { get; set; }
    }
}
namespace Entities.Models
{
    public class GoogleAnalyticsOptions
    {
        public string? PropertyId { get; set; }
        public string? ViewId { get; set; }
        public object? ServiceAccountKeyJson { get; set; }
    }
}
namespace Entities.DTOs.GoogleAnalyticsDto
{
    public class AnalyticsReportRequest
    {
        public string? PropertyId { get; set; } 
        public string? ViewId { get; set; }  
        public DateTime? StartDate { get; set; } = DateTime.UtcNow.AddDays(-30);
        public DateTime? EndDate { get; set; } = DateTime.UtcNow;
        public List<string> Metrics { get; set; } = new List<string>();
        public List<string> Dimensions { get; set; } = new List<string>();
        public int? PageSize { get; set; } = 10000;
        public string? PageToken { get; set; }
    }
}
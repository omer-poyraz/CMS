using Microsoft.AspNetCore.Http;

namespace Entities.DTOs.GoogleAnalyticsDto
{
    public abstract record GoogleAnalyticsDtoForManipulation
    {
        public string? PropertyId { get; set; }
        public string? ViewId { get; set; }
        public string? CustomDimensions { get; init; } 
        public string? CustomMetrics { get; init; } 
        public string? Configuration { get; init; }
        public bool Active { get; set; } = true;
        public IFormFile? ServiceAccountKeyFile { get; set; }
        public string? UserId { get; set; }
    }
}
using Microsoft.AspNetCore.Http;

namespace Entities.DTOs.GoogleAnalyticsDto
{
    public record GoogleAnalyticsDtoForInsertion : GoogleAnalyticsDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
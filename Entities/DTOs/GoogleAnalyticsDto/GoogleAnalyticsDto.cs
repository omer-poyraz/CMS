using Entities.Models;

namespace Entities.DTOs.GoogleAnalyticsDto
{
    public class GoogleAnalyticsDto
    {
        public int ID { get; init; }
        public string? PropertyId { get; init; }
        public string? ViewId { get; init; }
        public string? CustomDimensions { get; init; }
        public string? CustomMetrics { get; init; }
        public string? Configuration { get; init; }
        public bool Active { get; init; }
        public string? UserId { get; init; }
        public User? User { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
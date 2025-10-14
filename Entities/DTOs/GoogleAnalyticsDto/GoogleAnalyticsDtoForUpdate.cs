namespace Entities.DTOs.GoogleAnalyticsDto
{
    public record GoogleAnalyticsDtoForUpdate : GoogleAnalyticsDtoForManipulation
    {
        public int ID { get; init; }
        public bool? TrackChanges { get; init; } = false;
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
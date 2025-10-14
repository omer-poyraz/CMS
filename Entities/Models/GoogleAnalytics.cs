namespace Entities.Models
{
    public class GoogleAnalytics
    {
        public int ID { get; set; }
        public string? PropertyId { get; set; }
        public string? ViewId { get; set; }  
        public bool Active { get; set; } = true;
        public string? ServiceAccountKeyJson { get; set; } 
        public string? CustomDimensions { get; set; } 
        public string? CustomMetrics { get; set; } 
        public string? Configuration { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
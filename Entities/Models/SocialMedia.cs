namespace Entities.Models
{
    public class SocialMedia
    {
        public int ID { get; set; }
        public string? Files { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

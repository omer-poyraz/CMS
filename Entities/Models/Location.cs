namespace Entities.Models
{
    public class Location
    {
        public int ID { get; set; }
        public string? Files { get; set; }
        public string? Text { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public string? Url { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

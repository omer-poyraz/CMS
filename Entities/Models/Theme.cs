namespace Entities.Models
{
    public class Theme
    {
        public int ID { get; set; }
        public string? Files { get; set; }
        public string? BackgroundColor { get; set; }
        public string? FontFamily { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

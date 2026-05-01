namespace Entities.Models
{
    public class Logo
    {
        public int ID { get; set; }
        public string? Header { get; set; }
        public string? Footer { get; set; }
        public string? DarkHeader { get; set; }
        public string? DarkFooter { get; set; }
        public string? Other { get; set; }
        public string? OtherDark { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

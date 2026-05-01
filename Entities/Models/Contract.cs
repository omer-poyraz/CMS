namespace Entities.Models
{
    public class Contract
    {
        public int ID { get; set; }
        public string? Files { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

namespace Entities.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public string? Files { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Fax { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

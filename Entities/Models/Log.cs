namespace Entities.Models
{
    public class Log
    {
        public int ID { get; set; }
        public string? ServiceName { get; set; }
        public int? StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Process { get; set; }
        public string? Result { get; set; }
        public string? Ip { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

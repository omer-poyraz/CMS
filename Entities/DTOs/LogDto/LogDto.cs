using System.Text.Json;
using Entities.Models;

namespace Entities.DTOs.LogDto
{
    public class LogDto
    {
        public int ID { get; init; }
        public string? ServiceName { get; init; }
        public int? StatusCode { get; init; }
        public string? Message { get; init; }
        public string? Process { get; init; }
        public string? Result { get; init; }
        public string? Ip { get; init; }
        public string? UserId { get; init; }
        public User? User { get; init; }
        public DateTime? CreatedAt { get; init; }
    }
}

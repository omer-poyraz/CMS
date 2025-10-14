using System.Text.Json;
using Entities.Models;

namespace Entities.DTOs.ContentDto
{
    public class ContentDto
    {
        public int ID { get; init; }
        public string? Code { get; init; }
        public JsonDocument? Value { get; init; }
        public string? Type { get; init; }
        public JsonDocument? User { get; init; }

        public List<Product>? Products { get; set; }

        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}

using System.Text.Json;

namespace Entities.DTOs.LanguageDto
{
    public class LanguageDto
    {
        public int ID { get; init; }
        public string? Flag { get; init; }
        public string? Code { get; init; }
        public string? ZipCode { get; init; }
        public JsonDocument? User { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}

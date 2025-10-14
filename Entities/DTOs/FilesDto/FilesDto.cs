using System.Text.Json;
using Entities.Models;

namespace Entities.DTOs.FilesDto
{
    public class FilesDto
    {
        public int ID { get; init; }
        public string? FileUrl { get; init; }
        public string? FileType { get; init; }
        public bool? WaterMarked { get; init; }
        public JsonDocument? User { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}

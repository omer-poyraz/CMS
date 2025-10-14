using System.Text.Json;
using Entities.Models;

namespace Entities.DTOs.VersioningDto
{
    public class VersioningDto
    {
        public int ID { get; init; }
        public int? Version { get; init; }
        public JsonDocument? User { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}

using System.Text.Json;
using Entities.Models;

namespace Entities.DTOs.VideoGroupDto
{
    public class VideoGroupDto
    {
        public int ID { get; init; }
        public JsonDocument? Files { get; init; }
        public JsonDocument? Title { get; init; }
        public JsonDocument? Description { get; init; }
        public List<Video>? Videos { get; init; }
        public JsonDocument? User { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}

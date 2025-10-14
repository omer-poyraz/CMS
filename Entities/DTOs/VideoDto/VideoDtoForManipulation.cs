using System.Text.Json;

namespace Entities.DTOs.VideoDto
{
    public abstract record VideoDtoForManipulation
    {
        public JsonDocument? Files { get; set; }
        public JsonDocument? Title { get; set; }
        public JsonDocument? Description { get; set; }
        public int? VideoGroupID { get; set; }
    }
}

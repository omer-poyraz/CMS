using System.Text.Json;

namespace Entities.DTOs.VideoGroupDto
{
    public abstract record VideoGroupDtoForManipulation
    {
        public JsonDocument? Files { get; init; }
        public JsonDocument? Title { get; init; }
        public JsonDocument? Description { get; init; }
    }
}

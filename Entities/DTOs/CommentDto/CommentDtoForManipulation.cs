using System.Text.Json;

namespace Entities.DTOs.CommentDto
{
    public abstract record CommentDtoForManipulation
    {
        public JsonDocument? Files { get; init; }
        public JsonDocument? Title { get; init; }
        public JsonDocument? Description { get; init; }
        public JsonDocument? Content { get; init; }
        public string? UserId { get; init; }
    }
}

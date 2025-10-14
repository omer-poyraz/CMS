using System.Text.Json;

namespace Entities.DTOs.ContentDto
{
    public abstract record ContentDtoForManipulation
    {
        public string? Code { get; init; }
        public JsonDocument? Value { get; init; }
        public string? Type { get; init; }
    }
}

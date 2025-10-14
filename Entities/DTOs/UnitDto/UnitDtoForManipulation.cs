using System.Text.Json;

namespace Entities.DTOs.UnitDto
{
    public abstract record UnitDtoForManipulation
    {
        public JsonDocument? Files { get; init; }
        public JsonDocument? Title { get; init; }
        public JsonDocument? Description { get; init; }
        public JsonDocument? Content { get; init; }
    }
}

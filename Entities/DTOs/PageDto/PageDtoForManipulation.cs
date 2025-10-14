using System.Text.Json;

namespace Entities.DTOs.PageDto
{
    public abstract record PageDtoForManipulation
    {
        public JsonDocument? Slug { get; init; }
        public JsonDocument? Content { get; init; }
        public int? PopupID { get; init; }
        public JsonDocument? Meta { get; init; }
    }
}

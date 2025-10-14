using System.Text.Json;

namespace Entities.DTOs.MenuDto
{
    public abstract record MenuDtoForManipulation
    {
        public string? Icon { get; init; }
        public JsonDocument? Slug { get; init; }
        public JsonDocument? Title { get; init; }
        public JsonDocument? SpecialField1 { get; init; }
        public int? MenuGroupID { get; init; }
        public int? ParentMenuId { get; init; }
        public int? Sort { get; init; }
    }
}

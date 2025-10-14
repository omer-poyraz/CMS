using System.Text.Json;

namespace Entities.DTOs.MenuGroupDto
{
    public abstract record MenuGroupDtoForManipulation
    {
        public JsonDocument? Title { get; init; }
        public JsonDocument? SpecialField { get; init; }
    }
}

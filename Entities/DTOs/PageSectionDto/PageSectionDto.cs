using Entities.Models;

namespace Entities.DTOs.PageSectionDto
{
    public class PageSectionDto
    {
        public int ID { get; init; }
        public int? PageID { get; init; }
        public string? Type { get; init; }
        public int? Sort { get; init; }
        public List<SectionFieldDto.SectionFieldDto>? Fields { get; set; } = new();
        public List<SectionItemDto.SectionItemDto>? Items { get; set; } = new();
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}

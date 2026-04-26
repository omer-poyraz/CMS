using Entities.Models;

namespace Entities.DTOs.SectionItemDto
{
    public class SectionItemDto
    {
        public int ID { get; init; }
        public int? PageSectionID { get; init; }
        public int? Sort { get; init; }
        public List<SectionFieldDto.SectionFieldDto>? Fields { get; set; } = new();
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}

using Entities.Models;

namespace Entities.DTOs.MenuGroupDto
{
    public class MenuGroupDto
    {
        public int ID { get; init; }
        public ICollection<MenuGroupTranslation>? Translations { get; init; }
        public List<Menu>? Menus { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}

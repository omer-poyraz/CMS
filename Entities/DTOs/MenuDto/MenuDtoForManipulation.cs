using Entities.Models;

namespace Entities.DTOs.MenuDto
{
    public abstract record MenuDtoForManipulation
    {
        public int? MenuGroupID { get; set; }
        public int? ParentMenuID { get; set; }
        public ICollection<MenuTranslation>? Translations { get; set; }
        public int? Sort { get; set; }
    }
}

using Entities.Models;

namespace Entities.DTOs.MenuGroupDto
{
    public abstract record MenuGroupDtoForManipulation
    {
        public ICollection<MenuGroupTranslation>? Translations { get; init; }
    }
}

namespace Entities.DTOs.MenuGroupTranslationDto
{
    public record MenuGroupTranslationDtoForInsertion : MenuGroupTranslationDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}

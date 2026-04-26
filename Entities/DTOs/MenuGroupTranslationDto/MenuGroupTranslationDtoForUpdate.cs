namespace Entities.DTOs.MenuGroupTranslationDto
{
    public record MenuGroupTranslationDtoForUpdate : MenuGroupTranslationDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}

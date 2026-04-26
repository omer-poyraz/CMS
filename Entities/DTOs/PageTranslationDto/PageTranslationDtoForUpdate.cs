namespace Entities.DTOs.PageTranslationDto
{
    public record PageTranslationDtoForUpdate : PageTranslationDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}

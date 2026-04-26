namespace Entities.DTOs.PageTranslationDto
{
    public record PageTranslationDtoForInsertion : PageTranslationDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}

namespace Entities.DTOs.LanguageDto
{
    public record LanguageDtoForInsertion : LanguageDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}

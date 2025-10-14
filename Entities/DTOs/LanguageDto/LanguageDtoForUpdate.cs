namespace Entities.DTOs.LanguageDto
{
    public record LanguageDtoForUpdate : LanguageDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}

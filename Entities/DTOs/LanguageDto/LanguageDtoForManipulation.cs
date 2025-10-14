namespace Entities.DTOs.LanguageDto
{
    public abstract record LanguageDtoForManipulation
    {
        public string? Flag { get; init; }
        public string? Code { get; init; }
        public string? ZipCode { get; init; }
    }
}

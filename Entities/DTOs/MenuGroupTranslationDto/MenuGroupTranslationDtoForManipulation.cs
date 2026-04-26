namespace Entities.DTOs.MenuGroupTranslationDto
{
    public abstract record MenuGroupTranslationDtoForManipulation
    {
        public int MenuGroupID { get; set; }
        public string? Lang { get; set; }
        public string? File { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}

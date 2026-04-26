namespace Entities.DTOs.MenuGroupTranslationDto
{
    public class MenuGroupTranslationDto
    {
        public int ID { get; set; }
        public int MenuGroupID { get; set; }
        public string? Lang { get; set; }
        public string? File { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}

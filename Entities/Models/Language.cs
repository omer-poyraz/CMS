namespace Entities.Models
{
    public class Language
    {
        public int ID { get; set; }
        public ICollection<LanguageTranslation>? Translations { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

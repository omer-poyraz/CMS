using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class LanguageTranslation
    {
        public int ID { get; set; }
        [ForeignKey("LanguageID")]
        public Language? Language { get; set; }
        public int? LanguageID { get; set; }
        public string? Title { get; set; }
        public string? Lang { get; set; }
        public string? Flag { get; set; }
        public string? Code { get; set; }
        public string? ZipCode { get; set; }
    }
}

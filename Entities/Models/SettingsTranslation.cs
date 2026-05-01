using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class SettingsTranslation
    {
        public int ID { get; set; }
        public string? Lang { get; set; }
        [ForeignKey("SettingsID")]
        public Settings? Settings { get; set; }
        public int SettingsID { get; set; }
        public string? SiteName { get; set; }
        public ICollection<Logo>? Logos { get; set; }
        public Theme? Theme { get; set; }
        public ICollection<Contact>? Contacts { get; set; }
        public ICollection<Contract>? Contracts { get; set; }
        public ICollection<Location>? Locations { get; set; }
        public int? Menu { get; set; }
        public int? Footer { get; set; }
        public ICollection<Reference>? References { get; set; }
        public ICollection<SocialMedia>? SocialMedias { get; set; }
    }
}

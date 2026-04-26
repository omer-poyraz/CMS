namespace Entities.Models
{
    public class MenuGroup
    {
        public int ID { get; set; }
        public ICollection<MenuGroupTranslation>? Translations { get; set; }
        public List<Menu>? Menus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

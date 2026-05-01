using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class MenuTranslationConfig : IEntityTypeConfiguration<MenuTranslation>
    {
        public void Configure(EntityTypeBuilder<MenuTranslation> builder)
        {
            builder.HasKey(s => s.ID);
        }
    }
}

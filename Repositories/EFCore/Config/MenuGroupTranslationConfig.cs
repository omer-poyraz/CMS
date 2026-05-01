using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class MenuGroupTranslationConfig : IEntityTypeConfiguration<MenuGroupTranslation>
    {
        public void Configure(EntityTypeBuilder<MenuGroupTranslation> builder)
        {
            builder.HasKey(s => s.ID);
        }
    }
}

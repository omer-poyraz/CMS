using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class LanguageConfig : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(s => s.ID);

            builder.HasMany(s => s.Translations)
                .WithOne(t => t.Language)
                .HasForeignKey(t => t.LanguageID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

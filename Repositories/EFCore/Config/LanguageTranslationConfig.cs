using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class LanguageTranslationConfig : IEntityTypeConfiguration<LanguageTranslation>
    {
        public void Configure(EntityTypeBuilder<LanguageTranslation> builder)
        {
            builder.HasKey(s => s.ID);
        }
    }
}

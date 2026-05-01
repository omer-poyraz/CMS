using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class SettingsTranslationConfig : IEntityTypeConfiguration<SettingsTranslation>
    {
        public void Configure(EntityTypeBuilder<SettingsTranslation> builder)
        {
            builder.HasKey(s => s.ID);
        }
    }
}

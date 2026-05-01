using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class PageTranslationConfig : IEntityTypeConfiguration<PageTranslation>
    {
        public void Configure(EntityTypeBuilder<PageTranslation> builder)
        {
            builder.HasKey(s => s.ID);
        }
    }
}

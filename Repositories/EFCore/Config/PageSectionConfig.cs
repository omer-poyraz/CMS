using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class PageSectionConfig : IEntityTypeConfiguration<PageSection>
    {
        public void Configure(EntityTypeBuilder<PageSection> builder)
        {
            builder.HasKey(s => s.ID);
        }
    }
}

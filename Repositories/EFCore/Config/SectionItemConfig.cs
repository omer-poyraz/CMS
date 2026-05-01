using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class SectionItemConfig : IEntityTypeConfiguration<SectionItem>
    {
        public void Configure(EntityTypeBuilder<SectionItem> builder)
        {
            builder.HasKey(s => s.ID);
        }
    }
}

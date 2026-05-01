using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class SectionFieldConfig : IEntityTypeConfiguration<SectionField>
    {
        public void Configure(EntityTypeBuilder<SectionField> builder)
        {
            builder.HasKey(s => s.ID);
        }
    }
}

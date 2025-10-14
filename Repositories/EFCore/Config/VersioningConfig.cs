using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class VersioningConfig : IEntityTypeConfiguration<Versioning>
    {
        public void Configure(EntityTypeBuilder<Versioning> builder)
        {
            builder.HasKey(s => s.ID);
            builder.HasData(new Versioning { ID = 1, Version = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
        }
    }
}

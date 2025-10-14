using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class VideoGroupConfig : IEntityTypeConfiguration<VideoGroup>
    {
        public void Configure(EntityTypeBuilder<VideoGroup> builder)
        {
            builder.HasKey(s => s.ID);
        }
    }
}

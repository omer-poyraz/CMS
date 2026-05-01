using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class LogoConfig : IEntityTypeConfiguration<Logo>
    {
        public void Configure(EntityTypeBuilder<Logo> builder)
        {
            builder.HasKey(s => s.ID);
        }
    }
}

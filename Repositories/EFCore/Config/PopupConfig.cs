using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class PopupConfig : IEntityTypeConfiguration<Popup>
    {
        public void Configure(EntityTypeBuilder<Popup> builder)
        {
            builder.HasKey(s => s.ID);
        }
    }
}

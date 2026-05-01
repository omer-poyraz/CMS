using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Repositories.EFCore.Config
{
    public class GoogleAnalyticsConfig : IEntityTypeConfiguration<GoogleAnalytics>
    {
        public void Configure(EntityTypeBuilder<GoogleAnalytics> builder)
        {
            builder.HasKey(ga => ga.ID);
            builder.Property(p => p.CustomDimensions).HasConversion(
                v => v != null ? JsonConvert.SerializeObject(JsonConvert.DeserializeObject<Dictionary<string, string>>(v)) : null,
                v => v);
            builder.Property(p => p.CustomMetrics).HasConversion(
                v => v != null ? JsonConvert.SerializeObject(JsonConvert.DeserializeObject<Dictionary<string, string>>(v)) : null,
                v => v);
            builder.Property(p => p.Configuration).HasConversion(
                v => v != null ? JsonConvert.SerializeObject(JsonConvert.DeserializeObject<Dictionary<string, object>>(v)) : null,
                v => v);
        }
    }
}
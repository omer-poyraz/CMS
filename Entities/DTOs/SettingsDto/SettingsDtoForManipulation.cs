using System.Text.Json;

namespace Entities.DTOs.SettingsDto
{
    public abstract record SettingsDtoForManipulation
    {
        public JsonDocument? SiteName { get; init; }
        public JsonDocument? Files { get; init; }
        public JsonDocument? Meta { get; init; }
        public JsonDocument? Theme { get; init; }
        public JsonDocument? Contact { get; init; }
        public JsonDocument? Contracts { get; init; }
        public JsonDocument? Location { get; init; }
        public JsonDocument? Menu { get; init; }
        public JsonDocument? Footer { get; init; }
        public JsonDocument? References { get; init; }
        public JsonDocument? SocialMedias { get; init; }
    }
}

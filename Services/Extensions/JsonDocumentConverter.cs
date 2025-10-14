using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Services.Extensions
{
    public class JsonDocumentConverter : JsonConverter<JsonDocument>
    {
        public override JsonDocument Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument document = JsonDocument.ParseValue(ref reader))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(document.RootElement.GetRawText());
                return JsonDocument.Parse(bytes);
            }
        }

        public override void Write(Utf8JsonWriter writer, JsonDocument value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            try
            {
                JsonElement jsonElement = value.RootElement;
                JsonSerializer.Serialize(writer, jsonElement);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JsonDocument serialization error: {ex.Message}");
                writer.WriteNullValue();
            }
        }
    }
}
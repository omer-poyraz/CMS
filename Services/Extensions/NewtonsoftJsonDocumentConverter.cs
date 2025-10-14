using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Services.Extensions
{
    public class NewtonsoftJsonDocumentConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(JsonDocument) || objectType == typeof(JsonDocument);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var jToken = JToken.ReadFrom(reader);
            string json = jToken.ToString(Formatting.None);

            return JsonDocument.Parse(json);
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            if (value is JsonDocument jsonDoc)
            {
                try
                {
                    using var ms = new MemoryStream();
                    using var jsonWriter = new Utf8JsonWriter(ms);
                    jsonDoc.WriteTo(jsonWriter);
                    jsonWriter.Flush();

                    string json = Encoding.UTF8.GetString(ms.ToArray());

                    JToken jToken = JToken.Parse(json);

                    jToken.WriteTo(writer);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"JsonDocument serialization error: {ex.Message}");
                    writer.WriteNull();
                }
            }
        }
    }
}
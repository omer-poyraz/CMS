using System.Text;
using System.Text.Json;

public class ContentPrettyFormatMiddleware
{
    private readonly RequestDelegate _next;

    public ContentPrettyFormatMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.ContentType != null && context.Request.ContentType.Contains("application/json"))
        {
            context.Request.EnableBuffering();

            string body;
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            body = body.Trim();

            int firstCurly = body.IndexOf('{');
            int lastCurly = body.LastIndexOf('}');
            if (firstCurly >= 0 && lastCurly > firstCurly)
            {
                body = body.Substring(firstCurly, lastCurly - firstCurly + 1);
            }

            if (!string.IsNullOrWhiteSpace(body) && body.Contains("\"content\""))
            {
                try
                {
                    using var doc = JsonDocument.Parse(body);
                    var root = doc.RootElement;

                    if (root.TryGetProperty("content", out var contentElement))
                    {
                        var options = new JsonSerializerOptions { WriteIndented = true };
                        var formattedContent = JsonSerializer.Serialize(contentElement, options);

                        using var ms = new MemoryStream();
                        using (var writer = new Utf8JsonWriter(ms, new JsonWriterOptions { Indented = true }))
                        {
                            writer.WriteStartObject();
                            foreach (var prop in root.EnumerateObject())
                            {
                                if (prop.Name == "content")
                                {
                                    using var contentDoc = JsonDocument.Parse(formattedContent);
                                    writer.WritePropertyName("content");
                                    contentDoc.RootElement.WriteTo(writer);
                                }
                                else
                                {
                                    prop.WriteTo(writer);
                                }
                            }
                            writer.WriteEndObject();
                        }

                        ms.Position = 0;
                        var newBody = await new StreamReader(ms).ReadToEndAsync();

                        var bytes = Encoding.UTF8.GetBytes(newBody);
                        var newStream = new MemoryStream(bytes);
                        context.Request.Body = newStream;
                        context.Request.Body.Position = 0;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"JSON parsing error: {ex.Message}");
                    Console.WriteLine("BODY ERROR: [" + body + "]");
                    context.Request.Body.Position = 0;
                }
            }
        }

        await _next(context);
    }
}
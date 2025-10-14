using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public class LanguageMiddleware
    {
        private readonly RequestDelegate _next;
        public const string DefaultLanguage = "EN";

        public LanguageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var language = context.Request.Headers["Accept-Language"].FirstOrDefault()?.ToUpper() ?? DefaultLanguage;

            if (!IsLanguageSupported(language))
            {
                language = DefaultLanguage;
            }

            context.Items["Language"] = language;
            await _next(context);
        }

        private bool IsLanguageSupported(string language)
        {
            return new[] { "EN", "TR", "FR", "RU", "DE", "AR" }.Contains(language);
        }
    }
}
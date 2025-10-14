using System.Security.Claims;
using System.Text.Json;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.Controllers;
using Repositories.EFCore;

namespace API.Extensions
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogMiddleware> _logger;
        private readonly IServiceProvider _serviceProvider;

        private class ResponseModel
        {
            public string? Message { get; set; }
            public int StatusCode { get; set; }
            public object? Result { get; set; }
        }

        private class AuthResult
        {
            public string? UserId { get; set; }
            public string? Name { get; set; }
            public string? AccessToken { get; set; }
            public string? RefreshToken { get; set; }
        }

        public LogMiddleware(
            RequestDelegate next,
            ILogger<LogMiddleware> logger,
            IServiceProvider serviceProvider
        )
        {
            _next = next;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        private string? GetIpAddress(HttpContext context)
        {
            var ip =
                context.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                ?? context.Connection.RemoteIpAddress?.ToString();
            return ip;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<RepositoryContext>();

                using var memStream = new MemoryStream();
                context.Response.Body = memStream;

                var endpoint = context.GetEndpoint();
                var controllerActionDescriptor =
                    endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

                await _next(context);

                memStream.Seek(0, SeekOrigin.Begin);
                string responseBody = await new StreamReader(memStream).ReadToEndAsync();
                memStream.Seek(0, SeekOrigin.Begin);

                ResponseModel? responseObj = null;
                string? userId = null;

                try
                {
                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        responseObj = JsonSerializer.Deserialize<ResponseModel>(
                            responseBody,
                            options
                        );

                        if (
                            controllerActionDescriptor?.ControllerName == "Authentication"
                            && responseObj?.Result != null
                        )
                        {
                            var resultJson = responseObj.Result.ToString();
                            var authResult = JsonSerializer.Deserialize<AuthResult>(
                                resultJson!,
                                options
                            );
                            userId = authResult?.UserId;
                        }
                        else
                        {
                            userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        }
                    }
                }
                catch (JsonException ex)
                {
                    _logger.LogWarning(ex, "Failed to parse response body as JSON");
                }

                var log = new Log
                {
                    ServiceName = controllerActionDescriptor?.ControllerName,
                    StatusCode = context.Response.StatusCode,
                    Message = responseObj?.Message,
                    Process = controllerActionDescriptor?.ActionName,
                    Result = responseObj?.Result?.ToString(),
                    UserId = userId,
                    Ip = GetIpAddress(context),
                    CreatedAt = DateTime.UtcNow,
                };

                dbContext.Set<Log>().Add(log);
                await dbContext.SaveChangesAsync();

                await memStream.CopyToAsync(originalBodyStream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing request in LogMiddleware");
                context.Response.Body = originalBodyStream;
                throw;
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
    }

    public static class LogMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}

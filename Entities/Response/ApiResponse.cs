using Microsoft.AspNetCore.Http;

namespace Entities.Response
{
    public class ApiResponse<T>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string Message { get; }
        public int StatusCode { get; }
        public T? Result { get; }
        public bool IsSuccess { get; }

        private ApiResponse(
            IHttpContextAccessor httpContextAccessor,
            string message,
            int statusCode,
            T? result,
            bool isSuccess)
        {
            _httpContextAccessor = httpContextAccessor;
            Message = message;
            StatusCode = statusCode;
            Result = result;
            IsSuccess = isSuccess;
        }

        private string GetLanguageSuffix()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            return (httpContext?.Items["Language"]?.ToString() ?? "EN").ToUpper();
        }

        private string GetLocalizedMessage(string baseMessageProperty)
        {
            var languageSuffix = GetLanguageSuffix();
            var messageType = baseMessageProperty.Split('.')[0];
            var messageKey = baseMessageProperty.Split('.')[1];

            var property = typeof(Messages)
                .GetNestedType(messageType)
                ?.GetField($"{messageKey}{languageSuffix}");

            return property?.GetValue(null)?.ToString() ?? baseMessageProperty;
        }

        public static ApiResponse<T> CreateSuccess(
            IHttpContextAccessor httpContextAccessor,
            T? result,
            string messageKey = "Success.Retrieved",
            int statusCode = 200)
        {
            var instance = new ApiResponse<T>(
                httpContextAccessor: httpContextAccessor,
                message: new ApiResponse<T>(
                    httpContextAccessor,
                    messageKey,
                    statusCode,
                    result,
                    true
                ).GetLocalizedMessage(messageKey),
                statusCode: statusCode,
                result: result,
                isSuccess: true
            );

            return instance;
        }

        public static ApiResponse<T> CreateError(
            IHttpContextAccessor httpContextAccessor,
            string messageKey = "Error.ServerError",
            int statusCode = 400)
        {
            var instance = new ApiResponse<T>(
                httpContextAccessor: httpContextAccessor,
                message: new ApiResponse<T>(
                    httpContextAccessor,
                    messageKey,
                    statusCode,
                    default,
                    false
                ).GetLocalizedMessage(messageKey),
                statusCode: statusCode,
                result: default,
                isSuccess: false
            );

            return instance;
        }
    }
}
using System.Text.Json;

namespace Cust_middleware.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _nextMiddleware;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate nextMiddleware, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _nextMiddleware = nextMiddleware;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext http)
        {
            try
            {
                await _nextMiddleware(http);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(http, ex);
            }
        }

        public Task HandleExceptionAsync(HttpContext http, Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred while processing the request.");

            http.Response.ContentType = "application/json";
            http.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var Res = JsonSerializer.Serialize(new
            {
                error = ex.Message,
                stack = ex.StackTrace,
            });

            return http.Response.WriteAsync(Res);
        }
    }
}

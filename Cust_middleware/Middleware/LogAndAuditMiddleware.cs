using Cust_middleware.Models;
using System.Diagnostics;

namespace Cust_middleware.Middleware
{
    public class LogAndAuditMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<LogAndAuditMiddleware> _logger;

        public LogAndAuditMiddleware(RequestDelegate nextMiddleware, ILogger<LogAndAuditMiddleware> logger)
        {
            _next = nextMiddleware;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext http)
        {
            var sw = Stopwatch.StartNew();

            await _next(http);

            sw.Stop();

            var user = http.User?.Identity?.Name ?? "Anonymous";
            var ip = http.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

            _logger.LogInformation("Request {method} {url} => {status} in {time}ms",
                http.Request.Method,
                http.Request.Path + http.Request.QueryString,
                http.Response.StatusCode,
                sw.ElapsedMilliseconds);

            var scope = http.RequestServices.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<LiteContext>();

            var log = new RequestLog
            {
                Method = http.Request.Method,
                Path = http.Request.Path,
                QueryString = http.Request.QueryString.ToString(),
                StatusCode = http.Response.StatusCode,
                UserName = user,
                IpAddress = ip ?? "",
                ElapsedMilliseconds = sw.ElapsedMilliseconds,
                Timestamp = DateTime.UtcNow
            };

            db.RequestLogs.Add(log);
            await db.SaveChangesAsync();
        }
    }
}

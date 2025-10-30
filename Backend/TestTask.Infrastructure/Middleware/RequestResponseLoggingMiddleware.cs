using Microsoft.AspNetCore.Http;
using Serilog;

namespace TestTask.Infrastructure.Logging.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();
            var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;
            Log.Information("Request {Method} {Path} Body: {Body}", context.Request.Method, context.Request.Path, requestBody);

            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            Log.Information("Response {StatusCode} Body: {Body}", context.Response.StatusCode, responseText);

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Formatting.Compact;
using TestTask.Infrastructure.Middleware;

namespace TestTask.Infrastructure.DI.Setups
{
    public static class SerilogSetup
    {
        public static void Configure()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(new CompactJsonFormatter(), "Logs/log-.json", rollingInterval: RollingInterval.Day)
                .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri("http://elasticsearch:9200"))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = "app-logs-{0:yyyy.MM.dd}"
                })
                .CreateLogger();
        }

        public static void UseCustomSerilog(this WebApplication app)
        {
            app.UseSerilogRequestLogging(opts =>
            {
                opts.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    var user = httpContext.User.Identity?.Name ?? "Anonymous";
                    diagnosticContext.Set("User", user);
                    diagnosticContext.Set("Host", httpContext.Request.Host.Value);
                    diagnosticContext.Set("Protocol", httpContext.Request.Protocol);
                };
            });

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    if (exception != null)
                    {
                        Log.Error(exception, "Unhandled exception occurred");
                    }
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("Internal Server Error");
                });
            });

            app.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
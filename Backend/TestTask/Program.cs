using Library.WebAPI.Middlewares;
using Serilog;
using TestTask.Api.DI;
using TestTask.Infrastructure.DI;
using TestTask.Infrastructure.Logging;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddBusinessLogic(builder.Configuration);
builder.Host.ConfigureLogs(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
SeedRoles.InitializeRoles(app.Services).Wait();
await app.SeedAdminAsync();
app.UseCustomExceptionHandler();
app.UseCustomSerilog();
app.MapControllers();

app.Run();

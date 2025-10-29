using MediatR;
using System.Reflection;
using TestTask.Api.DI;
using TestTask.Infrastructure.DI;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(Assembly.Load("TestTask.Infrastructure"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBusinessLogic(builder.Configuration);

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
Seed.InitializeRoles(app.Services).Wait();


app.MapControllers();

app.Run();

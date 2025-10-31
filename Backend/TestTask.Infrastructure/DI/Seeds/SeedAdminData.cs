using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.DTOs.Identity;
using TestTask.Domain.Enums;
using TestTask.Infrastructure.Features.Users.Commands;
using Microsoft.AspNetCore.Identity;
using TestTask.Domain.Entities;

namespace TestTask.Infrastructure.DI.Seeds
{
    public static class SeedAdminData
    {
        public static async Task SeedAdminAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            var anyUsers = userManager.Users.Any();
            if (anyUsers)
                return;

            var adminSection = config.GetSection("FirstAdmin");
            var email = adminSection["Email"];
            var password = adminSection["Password"];
            var nickname = adminSection["Nickname"];

            var request = new RegisterRequest
            {
                Email = email!,
                Password = password!,
                PasswordConfirm = password!,
                Nickname = nickname ?? "Admin",
                Role = Roles.Admin
            };

            var command = new RegisterCommand { RegisterRequest = request };
            await mediator.Send(command);
        }
    }
}

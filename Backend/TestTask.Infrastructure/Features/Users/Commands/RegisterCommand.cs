using MediatR;
using TestTask.Application.DTOs.Identity;

namespace TestTask.Infrastructure.Features.Users.Commands
{
    public class RegisterCommand : IRequest<AuthResponse>
    {
        public RegisterRequest RegisterRequest { get; set; }
    }
}

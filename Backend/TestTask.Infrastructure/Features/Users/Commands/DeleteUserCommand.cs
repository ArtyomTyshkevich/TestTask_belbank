using MediatR;

namespace TestTask.Infrastructure.Features.Users.Commands
{
    public record DeleteUserCommand(string Email) : IRequest;
}

using MediatR;

namespace TestTask.Infrastructure.Features.Users.Commands
{
    public class RevokeTokenCommand : IRequest<Unit>
    {
        public string Username { get; set; }
    }

    public class RevokeAllTokensCommand : IRequest<Unit>
    {
    }
}

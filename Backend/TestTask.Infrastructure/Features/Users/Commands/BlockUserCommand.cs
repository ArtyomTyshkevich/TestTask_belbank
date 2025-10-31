using MediatR;

namespace TestTask.Infrastructure.Features.Users.Commands
{
    public class BlockUserCommand : IRequest
    {
        public string Email { get; set; }

        public BlockUserCommand(string email)
        {
            Email = email;
        }
    }
}

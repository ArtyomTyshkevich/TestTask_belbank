using MediatR;

namespace TestTask.Infrastructure.Features.Users.Commands
{
    public class UnblockUserCommand : IRequest
    {
        public string Email { get; set; }


        public UnblockUserCommand(string email)
        {
            Email = email;
        }
    }
}

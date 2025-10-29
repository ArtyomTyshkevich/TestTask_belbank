using MediatR;
using TestTask.Application.DTOs.Identity;

namespace TestTask.Infrastructure.Features.Users.Commands
{
    public class SetPasswordCommand : IRequest<bool>
    {
        public SetPasswordRequest Request { get; set; } = null!;
    }
}

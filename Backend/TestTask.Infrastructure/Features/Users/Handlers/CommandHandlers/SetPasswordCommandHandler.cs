using MediatR;
using Microsoft.AspNetCore.Identity;
using TestTask.Domain.Entities;
using TestTask.Infrastructure.Exceptions;
using TestTask.Infrastructure.Features.Users.Commands;

namespace TestTask.Infrastructure.Features.Users.Handlers.CommandHandlers
{
    public class SetPasswordCommandHandler : IRequestHandler<SetPasswordCommand, bool>
    {
        private readonly UserManager<User> _userManager;

        public SetPasswordCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Request.Email);
            if (user == null)
                throw new UserNotFoundException(request.Request.Email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.Request.NewPassword);


            return true;
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Identity;
using TestTask.Application.DTOs.Identity;
using TestTask.Infrastructure.Exceptions;
using TestTask.Domain.Entities;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Infrastructure.Features.Users.Commands;
using TestTask.Infrastructure.Features.Users.Queries;

namespace TestTask.Infrastructure.Features.Users.Handlers.CommandHandlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public RegisterCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = CreateRegisterUser(request.RegisterRequest);
            var result = await _unitOfWork.UserManagers.CreateUserAsync(user, request.RegisterRequest.Password);
            if (!result.Succeeded) throw new UserCreationFailedException();
            await _unitOfWork.UserManagers.AddToRoleAsync(user, request.RegisterRequest.Role);
            var authQuery = new AuthenticateQuery
            {
                AuthRequest = new AuthRequest
                {
                    Email = request.RegisterRequest.Email,
                    Password = request.RegisterRequest.Password
                }
            };
            return await _mediator.Send(authQuery, cancellationToken);
        }

        private User CreateRegisterUser(RegisterRequest request)
        {
            return new User
            {
                Nickname = request.Nickname,
                Email = request.Email,
                UserName = request.Email
            };
        }

    }
}

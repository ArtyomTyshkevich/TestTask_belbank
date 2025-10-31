using MediatR;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Domain.Enums;
using TestTask.Infrastructure.Features.Users.Commands;

namespace TestTask.Infrastructure.Features.Users.Handlers.CommandHandlers
{
    public class UnblockUserCommandHandler : IRequestHandler<UnblockUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnblockUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UnblockUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserManagers.FindByEmailAsync(request.Email);

            var roles = await _unitOfWork.UserManagers.GetRolesAsync(user);
            if (!roles.Contains(Roles.Blocked.ToString()))
                throw new InvalidOperationException($"User {request.Email} is not blocked.");

            await _unitOfWork.UserManagers.SetSingleRoleAsync(user, Roles.User);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

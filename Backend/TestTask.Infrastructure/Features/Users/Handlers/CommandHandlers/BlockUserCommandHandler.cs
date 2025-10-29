using MediatR;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Domain.Enums;
using TestTask.Infrastructure.Features.Users.Commands;

namespace TestTask.Infrastructure.Features.Users.Handlers.CommandHandlers
{
    public class BlockUserCommandHandler : IRequestHandler<BlockUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlockUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(BlockUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserManagers.FindByEmailAsync(request.Email);
            await _unitOfWork.UserManagers.SetSingleRoleAsync(user, Roles.Blocked);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

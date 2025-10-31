using MediatR;
using TestTask.Application.DTOs;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Infrastructure.Features.Users.Queries;

namespace TestTask.Infrastructure.Features.Users.Handlers.QuerieHandlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDTO>>
    {
        private readonly IUserManagerRepository _userManagerRepository;

        public GetAllUsersQueryHandler(IUserManagerRepository userManagerRepository)
        {
            _userManagerRepository = userManagerRepository;
        }

        public async Task<List<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userManagerRepository.GetAllUsersWithRolesAsync();
        }
    }
}

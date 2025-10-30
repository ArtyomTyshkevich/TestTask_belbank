using MediatR;
using TestTask.Application.DTOs;

namespace TestTask.Infrastructure.Features.Users.Queries
{
    public class GetAllUsersQuery : IRequest<List<UserDto>> { }
}

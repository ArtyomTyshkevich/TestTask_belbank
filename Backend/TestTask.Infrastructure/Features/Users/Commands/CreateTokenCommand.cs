using MediatR;
using Microsoft.AspNetCore.Identity;
using TestTask.Domain.Entities;

namespace TestTask.Infrastructure.Features.Users.Commands
{
    public class CreateTokenCommand : IRequest<string>
    {
        public User User { get; set; }
        public List<IdentityRole<Guid>> Roles { get; set; }
    }
}

using MediatR;
using Microsoft.AspNetCore.Identity;
using TestTask.Application.DTOs.Identity;

namespace TestTask.Infrastructure.Features.Users.Queries
{
    public class AuthenticateQuery : IRequest<AuthResponse>
    {
        public AuthRequest AuthRequest { get; set; }
        public List<IdentityRole<long>> Roles { get; set; }
    }
}

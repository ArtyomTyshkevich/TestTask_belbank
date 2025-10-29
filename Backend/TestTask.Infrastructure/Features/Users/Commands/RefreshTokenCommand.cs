using MediatR;
using Microsoft.AspNetCore.Identity;
using TestTask.Application.DTOs.Identity;

namespace TestTask.Infrastructure.Features.Users.Commands
{
    public class RefreshTokenCommand : IRequest<TokenModel>
    {
        public TokenModel tokenModel { get; set; }
        public List<IdentityRole<long>> Roles { get; set; }
    }
}

using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Domain.Entities;
using TestTask.Application.DTOs.Identity;
using TestTask.Infrastructure.Exceptions;
using TestTask.Infrastructure.Features.Users.Commands;
using TestTask.Infrastructure.Features.Users.Extensions;

namespace TestTask.Infrastructure.Features.Users.Handlers.CommandHandlers
{
    public class RefreshTokenComandHandler : IRequestHandler<RefreshTokenCommand, TokenModel>
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenComandHandler(IMediator mediator, UserManager<User> userManager, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _configuration = configuration;
        }

        public async Task<TokenModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _configuration.GetPrincipalFromExpiredToken(request.tokenModel.AccessToken);
            if (principal == null) throw new TokenInvalidException();
            var user = await GetUserAndValidateRefreshToken(principal, request.tokenModel.RefreshToken!, cancellationToken);
            var newAccessToken = _configuration.CreateToken(principal.Claims.ToList());
            var newRefreshToken = _configuration.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _unitOfWork.UserManagers.UpdateUserAsync(user);

            return new TokenModel { AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken), RefreshToken = newRefreshToken };
        }

        private async Task<User> GetUserAndValidateRefreshToken(ClaimsPrincipal principal, string refreshToken, CancellationToken cancellationToken)
        {
            var username = principal.Identity!.Name;
            var user = await _unitOfWork.UserManagers.FindByNameAsync(username!);
            if (user == null || user.RefreshToken != refreshToken)
            {
                throw new RefreshTokenInvalidException();
            }
            return user;
        }

    }
}

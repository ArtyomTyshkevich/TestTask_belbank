using MediatR;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using TestTask.Infrastructure.Features.Users.Commands;
using TestTask.Infrastructure.Features.Users.Extensions;

namespace TestTask.Infrastructure.Features.Users.Handlers.CommandHandlers
{
    public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, string>
    {
        private readonly IConfiguration _configuration;

        public CreateTokenCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            var token = request.User
                .CreateClaims(request.Roles)
                .CreateJwtToken(_configuration);
            var tokenHandler = new JwtSecurityTokenHandler();
            return await Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}

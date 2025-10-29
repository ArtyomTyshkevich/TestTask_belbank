using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Application.DTOs.Identity;
using TestTask.Infrastructure.Features.Users.Commands;
using TestTask.Infrastructure.Features.Users.Queries;

namespace TestTask.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request, CancellationToken cancellationToken)
        {
            var query = new AuthenticateQuery { AuthRequest = request };
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        [HttpPost("register")]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var query = new RegisterCommand { RegisterRequest = request };
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel, CancellationToken cancellationToken)
        {
            var command = new RefreshTokenCommand { tokenModel = tokenModel };
            var newTokens = await _mediator.Send(command, cancellationToken);
            return Ok(newTokens);
        }

        [HttpPost("revoke/{username}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Revoke(string username, CancellationToken cancellationToken)
        {
            var command = new RevokeTokenCommand { Username = username };
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPost("revoke-all")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> RevokeAll(CancellationToken cancellationToken)
        {
            var command = new RevokeAllTokensCommand();
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPost("set-password")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> SetPassword([FromBody] SetPasswordRequest request, CancellationToken cancellationToken)
        {
            var command = new SetPasswordCommand { Request = request };
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPost("{email}/block")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> BlockUser(string email, CancellationToken cancellationToken)
        {
            var command = new BlockUserCommand(email);
            await _mediator.Send(command, cancellationToken);
            return Ok(new { Message = "User blocked successfully" });
        }

        [HttpDelete("{email}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteUser(string email, CancellationToken cancellationToken)
        {
            var command = new DeleteUserCommand(email);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}

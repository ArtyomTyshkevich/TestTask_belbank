using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestTask.Infrastructure.Features.Currency.Queries;

namespace TestTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("usd")]
        public async Task<IActionResult> GetUsd([FromQuery] decimal amountInByn)
        {
            if (amountInByn <= 0)
                return BadRequest("Amount must be greater than zero.");

            var usdAmount = await _mediator.Send(new GetUsdRateQuery { AmountInByn = amountInByn });
            return Ok(new {AmountInUsd = usdAmount });
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Application.DTOs;
using TestTask.Infrastructure.Features.Products.Commands;
using TestTask.Infrastructure.Features.Products.Queries;

namespace TestTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("filter")]
        [Authorize(Policy = "UserOrAdvancedUser")]
        public async Task<ActionResult<List<ProductDTO>>> Filter(
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] Guid? categoryId,
            [FromQuery] string? nameStartsWith,
            CancellationToken cancellationToken)
        {
            var query = new FilterProductsQuery(minPrice, maxPrice, categoryId, nameStartsWith);
            var result = await _mediator.Send(query, cancellationToken);

            if (User.IsInRole("User"))
            {
                foreach (var p in result)
                    p.SpecialNote = null;
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "UserOrAdvancedUser")]
        public async Task<ActionResult<ProductDTO>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);

            if (User.IsInRole("User"))
                result.SpecialNote = null;

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = "UserOrAdvancedUser")]
        public async Task<ActionResult<ProductUpsertDTO>> Create(ProductUpsertDTO createProductDTO, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateProductCommand(createProductDTO), cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "UserOrAdvancedUser")]
        public async Task<ActionResult<ProductDTO>> Update(Guid id, [FromBody] ProductUpsertDTO productDto, CancellationToken cancellationToken)
        {
            var command = new UpdateProductCommand(id, productDto);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdvancedUserPolicy")]
        public async Task<ActionResult<bool>> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id), cancellationToken);
            return Ok(result);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Application.DTOs;
using TestTask.Infrastructure.Features.Categories.Commands;
using TestTask.Infrastructure.Features.Categories.Queries;

namespace TestTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllCategoriesQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetById(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetCategoryByIdQuery(id), cancellationToken));
        }

        [HttpPost]
        [Authorize(Policy = "AdvancedUserPolicy")]
        public async Task<ActionResult<CategoryDTO>> Create([FromBody] CategoryDTO categoryDto, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new CreateCategoryCommand(categoryDto), cancellationToken));
        }

        [HttpPut]
        [Authorize(Policy = "AdvancedUserPolicy")]
        public async Task<ActionResult<CategoryDTO>> Update([FromBody] CategoryDTO categoryDto, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new UpdateCategoryCommand(categoryDto), cancellationToken));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdvancedUserPolicy")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCategoryCommand(id), cancellationToken);
            return NoContent();
        }
    }
}

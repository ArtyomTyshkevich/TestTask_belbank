using MediatR;
using TestTask.Application.DTOs;

namespace TestTask.Infrastructure.Features.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryDTO?>
    {
        public Guid Id { get; set; }

        public GetCategoryByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
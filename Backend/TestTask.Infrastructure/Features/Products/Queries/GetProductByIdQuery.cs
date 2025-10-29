using MediatR;
using TestTask.Application.DTOs;

namespace TestTask.Infrastructure.Features.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDTO?>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
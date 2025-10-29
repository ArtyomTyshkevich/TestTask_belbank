using MediatR;
using TestTask.Application.DTOs;

namespace TestTask.Infrastructure.Features.Products.Commands
{
    public class CreateProductCommand : IRequest<ProductDTO>
    {
        public ProductUpsertDTO Product { get; }

        public CreateProductCommand(ProductUpsertDTO product)
        {
            Product = product;
        }
    }
}

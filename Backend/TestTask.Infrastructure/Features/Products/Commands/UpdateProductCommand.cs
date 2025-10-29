using MediatR;
using TestTask.Application.DTOs;
using System;

namespace TestTask.Infrastructure.Features.Products.Commands
{
    public class UpdateProductCommand : IRequest<ProductDTO>
    {
        public Guid ProductId { get; }
        public ProductUpsertDTO Product { get; }

        public UpdateProductCommand(Guid productId, ProductUpsertDTO product)
        {
            ProductId = productId;
            Product = product;
        }
    }
}

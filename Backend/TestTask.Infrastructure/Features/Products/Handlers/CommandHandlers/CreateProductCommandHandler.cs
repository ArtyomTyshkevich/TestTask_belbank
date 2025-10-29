using AutoMapper;
using MediatR;
using TestTask.Application.DTOs;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Domain.Entities;
using TestTask.Infrastructure.Exceptions;
using TestTask.Infrastructure.Features.Products.Commands;

namespace TestTask.Infrastructure.Features.Products.Handlers.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(request.Product.CategoryId, cancellationToken);

            if (category == null)
                throw new CategoryNotFoundException(request.Product.CategoryId);

            var product = _mapper.Map<Product>(request.Product);
            product.Category = category;

            await _unitOfWork.Products.AddAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductDTO>(product);
        }
    }
}

using AutoMapper;
using MediatR;
using TestTask.Application.DTOs;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Infrastructure.Exceptions;
using TestTask.Infrastructure.Features.Products.Commands;

namespace TestTask.Infrastructure.Features.Products.Handlers.CommandHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId, cancellationToken);

            var category = await _unitOfWork.Categories.GetByIdAsync(request.Product.CategoryId, cancellationToken);
            if (category == null)
                throw new CategoryNotFoundException(request.Product.CategoryId);
            _mapper.Map(request.Product, product);
            product.Category = category;

            await _unitOfWork.Products.UpdateAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductDTO>(product);
        }
    }
}

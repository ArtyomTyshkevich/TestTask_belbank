using AutoMapper;
using MediatR;
using TestTask.Application.DTOs;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Infrastructure.Features.Products.Queries;

namespace TestTask.Infrastructure.Features.Products.Handlers.QueryHandlers
{
    public class FilterProductsQueryHandler : IRequestHandler<FilterProductsQuery, List<ProductDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FilterProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> Handle(FilterProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Products.FilterAsync(
                request.MinPrice,
                request.MaxPrice,
                request.CategoryId,
                request.NameStartsWith,
                cancellationToken
            );

            return _mapper.Map<List<ProductDTO>>(products);
        }
    }
}

using MediatR;
using TestTask.Application.DTOs;

namespace TestTask.Infrastructure.Features.Products.Queries
{
    public class FilterProductsQuery : IRequest<List<ProductDTO>>
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public Guid? CategoryId { get; set; }
        public string? NameStartsWith { get; set; }

        public FilterProductsQuery(decimal? minPrice = null, decimal? maxPrice = null, Guid? categoryId = null, string? nameStartsWith = null)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            CategoryId = categoryId;
            NameStartsWith = nameStartsWith;
        }
    }
}

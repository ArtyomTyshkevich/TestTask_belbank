using TestTask.Domain.Entities;

namespace TestTask.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product, CancellationToken cancellationToken);
        Task DeleteAsync(Product product, CancellationToken cancellationToken);
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(Product product, CancellationToken cancellationToken);
        Task<List<Product>> FilterAsync(decimal? minPrice, decimal? maxPrice, Guid? categoryId, string? nameStartsWith, CancellationToken cancellationToken);
    }
}

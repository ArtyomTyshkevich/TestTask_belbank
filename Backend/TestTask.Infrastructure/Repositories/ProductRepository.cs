using TestTask.Domain.Entities;
using TestTask.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Interfaces.Repositories;

namespace TestTask.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TestTaskDbContext _context;

        public ProductRepository(TestTaskDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Product product, CancellationToken cancellationToken)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Product>> FilterAsync(decimal? minPrice, decimal? maxPrice, Guid? categoryId, string? nameStartsWith, CancellationToken cancellationToken)
        {
            var products = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (minPrice.HasValue)
                products = products.Where(p => p.PriceRub >= minPrice.Value);

            if (maxPrice.HasValue)
                products = products.Where(p => p.PriceRub <= maxPrice.Value);

            if (categoryId.HasValue)
                products = products.Where(p => p.Category.Id == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(nameStartsWith))
                products = products.Where(p => p.Name.ToLower().StartsWith(nameStartsWith.ToLower()));

            return await products.ToListAsync(cancellationToken);
        }

    }
}
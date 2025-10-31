using TestTask.Domain.Entities;
using TestTask.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Interfaces.Repositories;

namespace TestTask.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TestTaskDbContext _context;

        public CategoryRepository(TestTaskDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Categories.ToListAsync(cancellationToken);
        }

        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task AddAsync(Category category, CancellationToken cancellationToken)
        {
            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Category category, CancellationToken cancellationToken)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Category> GetByIdWithProductsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task DeleteAsync(Category category, CancellationToken cancellationToken)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
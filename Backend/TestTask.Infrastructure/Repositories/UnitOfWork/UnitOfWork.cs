
using TestTask.Application.Interfaces.Repositories;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Infrastructure.Contexts;

namespace TestTask.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestTaskDbContext _dbContext;

        public ICategoryRepository Categories { get; }
        public IProductRepository Products { get; }
        public IUserRepository Users { get; }
        public IUserManagerRepository UserManagers { get; }

        public UnitOfWork(
            TestTaskDbContext dbContext,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            IUserRepository userRepository,
            IUserManagerRepository userManagerRepository)
        {
            _dbContext = dbContext;
            Categories = categoryRepository;
            Products = productRepository;
            Users = userRepository;
            UserManagers = userManagerRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

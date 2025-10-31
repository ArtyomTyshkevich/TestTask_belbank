namespace TestTask.Application.Interfaces.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        IUserRepository Users { get; }
        IUserManagerRepository UserManagers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

using Microsoft.AspNetCore.Identity;
using TestTask.Domain.Entities;

namespace TestTask.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAsync(CancellationToken cancellationToken);
        Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<List<IdentityRole<Guid>>> GetRolesAsync(User user, CancellationToken cancellationToken);
        Task<User> GetByEmailAsync(string mail, CancellationToken cancellationToken);
        Task DeleteAsync(Guid userId, CancellationToken cancellationToken = default);
        Task UpdateAsync(User user, CancellationToken cancellationToken = default);
    }
}

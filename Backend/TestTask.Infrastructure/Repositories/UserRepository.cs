using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Domain.Entities;
using TestTask.Infrastructure.Contexts;

namespace TestTask.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TestTaskDbContext _authDbContext;

        public UserRepository(TestTaskDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public async Task<List<User>> GetAsync(CancellationToken cancellationToken)
        {
            return await _authDbContext.Users
                                            .Include(user => user.UserName)
                                            .ToListAsync(cancellationToken);
        }

        public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _authDbContext.Users
                                 .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
        }

        public async Task<List<IdentityRole<Guid>>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            var roles = await _authDbContext.Roles
                .Where(role => _authDbContext.UserRoles
                    .Where(contextUser => contextUser.UserId == user.Id)
                    .Select(contextUser => contextUser.RoleId)
                    .Contains(role.Id))
                .ToListAsync(cancellationToken);

            return roles;
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _authDbContext.Users
                .FirstAsync(user => user.Email == email, cancellationToken);
        }
        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            _authDbContext.Users
                .Update(user);
        }

        public async Task DeleteAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var user = await _authDbContext.Users
                .FindAsync(userId);

            if (user != null)
            {
                _authDbContext.Users
                    .Remove(user);
            }
        }
    }
}

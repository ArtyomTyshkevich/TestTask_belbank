using Microsoft.AspNetCore.Identity;
using TestTask.Application.DTOs;
using TestTask.Domain.Entities;
using TestTask.Domain.Enums;

namespace TestTask.Application.Interfaces.Repositories
{
    public interface IUserManagerRepository
    {
        Task<IdentityResult> CreateUserAsync(User user, string password);
        Task<User> FindByNameAsync(string name);
        Task<User> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<IdentityResult> UpdateUserAsync(User user);
        Task<IdentityResult> DeleteUserAsync(User user);
        Task<IdentityResult> AddToRoleAsync(User user, Roles role);
        Task<List<User>> UsersToListAsync(CancellationToken cancellationToken);
        Task<IList<string>> GetRolesAsync(User user);
        Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles);
        Task<IdentityResult> SetSingleRoleAsync(User user, Roles role);
        Task<List<UserDTO>> GetAllUsersWithRolesAsync();
    }
}

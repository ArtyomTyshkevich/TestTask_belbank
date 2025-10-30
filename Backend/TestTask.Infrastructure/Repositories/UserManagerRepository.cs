using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.DTOs;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Domain.Entities;
using TestTask.Domain.Enums;

namespace TestTask.Infrastructure.Repositories
{
    public class UserManagerRepository : IUserManagerRepository
    {
        private readonly UserManager<User> _userManager;

        public UserManagerRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> FindByNameAsync(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(User user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, Roles role)
        {
            var roleName = role.ToString();
            return await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<List<User>> UsersToListAsync(CancellationToken cancellationToken)
        {
            return await _userManager.Users.ToListAsync(cancellationToken);
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles)
        {
            return await _userManager.RemoveFromRolesAsync(user, roles);
        }

        public async Task<IdentityResult> SetSingleRoleAsync(User user, Roles role)
        {
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
            }
            return await AddToRoleAsync(user, role);
        }
        public async Task<List<UserDto>> GetAllUsersWithRolesAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                Roles userRole;

                if (Enum.TryParse(roles.FirstOrDefault(), out Roles parsedRole))
                    userRole = parsedRole;
                else
                    userRole = Roles.User;

                userDtos.Add(new UserDto
                {
                    Nickname = user.Nickname,
                    Email = user.Email,
                    Role = userRole
                });
            }

            return userDtos;
        }
    }
}

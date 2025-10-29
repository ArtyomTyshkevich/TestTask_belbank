
using Microsoft.AspNetCore.Identity;

namespace TestTask.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Nickname { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

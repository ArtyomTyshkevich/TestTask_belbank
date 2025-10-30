using TestTask.Domain.Enums;

namespace TestTask.Application.DTOs
{
    public class UserDto
    {
        public string Nickname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Roles Role { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using TestTask.Domain.Enums;

namespace TestTask.Application.DTOs.Identity
{
    public class RegisterRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PasswordConfirm { get; set; } = null!;
        public string Nickname { get; set; } = null!;
        public Roles Role { get; set; }
    }
}

namespace TestTask.Application.DTOs.Identity
{
    public class SetPasswordRequest
    {
        public string Email { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}

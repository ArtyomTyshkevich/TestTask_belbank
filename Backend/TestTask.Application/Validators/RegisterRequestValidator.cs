using FluentValidation;
using TestTask.Application.DTOs.Identity;

namespace TestTask.Application.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.PasswordConfirm)
                .Equal(x => x.Password).WithMessage("Passwords do not match.");

            RuleFor(x => x.Nickname)
                .NotEmpty().WithMessage("Nickname is required.")
                .Length(3, 50).WithMessage("Nickname must be between 3 and 50 characters.");
        }
    }
}

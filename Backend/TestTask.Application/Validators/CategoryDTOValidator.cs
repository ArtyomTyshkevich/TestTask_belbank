using FluentValidation;
using TestTask.Application.DTOs;

namespace TestTask.Application.Validators
{
    public class CategoryDTOValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(50).WithMessage("Category name must not exceed 50 characters.");

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Category ID is required.");
        }
    }
}

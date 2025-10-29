using FluentValidation;
using TestTask.Application.DTOs;

namespace TestTask.Application.Validators
{
    public class BaseProductValidator<T> : AbstractValidator<T>
        where T : class
    {
        public BaseProductValidator()
        {
            RuleFor(x => (x as ProductUpsertDTO)!.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters.")
                .When(x => x is ProductUpsertDTO);

            RuleFor(x => (x as ProductUpsertDTO)!.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(300).WithMessage("Description must not exceed 300 characters.")
                .When(x => x is ProductUpsertDTO);

            RuleFor(x => (x as ProductUpsertDTO)!.PriceRub)
                .GreaterThan(0).WithMessage("Price must be greater than 0.")
                .LessThanOrEqualTo(1_000_000).WithMessage("Price is too high.")
                .When(x => x is ProductUpsertDTO);

            RuleFor(x => (x as ProductUpsertDTO)!.CommonNote)
                .MaximumLength(200).WithMessage("Common note must not exceed 200 characters.")
                .When(x => x is ProductUpsertDTO);

            RuleFor(x => (x as ProductUpsertDTO)!.SpecialNote)
                .MaximumLength(200).WithMessage("Special note must not exceed 200 characters.")
                .When(x => x is ProductUpsertDTO);

            RuleFor(x => (x as ProductUpsertDTO)!.CategoryId)
                .NotNull().WithMessage("Category is required.")
                .When(x => x is ProductUpsertDTO);
        }
    }

    public class CreateProductDTOValidator : BaseProductValidator<ProductUpsertDTO>
    {
        public CreateProductDTOValidator()
        {
        }
    }

    public class ProductDTOValidator : BaseProductValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
        }
    }
}

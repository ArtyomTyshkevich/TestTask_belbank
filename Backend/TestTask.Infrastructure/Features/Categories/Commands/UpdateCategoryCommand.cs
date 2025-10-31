using MediatR;
using TestTask.Application.DTOs;

namespace TestTask.Infrastructure.Features.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<CategoryDTO?>
    {
        public CategoryDTO Category { get; set; }
        public UpdateCategoryCommand(CategoryDTO category)
        {
            Category = category;
        }
    }
}
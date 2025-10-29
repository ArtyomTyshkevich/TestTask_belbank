using MediatR;
using TestTask.Application.DTOs;

namespace TestTask.Infrastructure.Features.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryDTO>
    {
        public CategoryDTO Category { get; set; }
        public CreateCategoryCommand(CategoryDTO category)
        { 
            Category = category;
        }
    }
}
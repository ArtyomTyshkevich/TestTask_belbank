using MediatR;
using TestTask.Application.DTOs;

namespace TestTask.Infrastructure.Features.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryDTO>>
    {
    }
}
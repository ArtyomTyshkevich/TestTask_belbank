using MediatR;
using TestTask.Application.DTOs;
using System.Collections.Generic;

namespace TestTask.Infrastructure.Features.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryDTO>>
    {
    }
}
using TestTask.Domain.Entities;

namespace TestTask.Application.DTOs
{
    public class CategoryDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
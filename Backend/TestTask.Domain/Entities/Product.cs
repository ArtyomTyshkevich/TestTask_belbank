
using System.ComponentModel.DataAnnotations;

namespace TestTask.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal PriceRub { get; set; }
        public string? CommonNote { get; set; }
        public string? SpecialNote { get; set; }
        public Category Category { get; set; } = null!;
    }

}

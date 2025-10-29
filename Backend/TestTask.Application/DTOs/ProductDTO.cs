namespace TestTask.Application.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal PriceRub { get; set; }
        public string? CommonNote { get; set; }
        public string? SpecialNote { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
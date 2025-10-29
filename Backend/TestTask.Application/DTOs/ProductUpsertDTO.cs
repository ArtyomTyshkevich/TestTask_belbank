namespace TestTask.Application.DTOs
{
    public class ProductUpsertDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal PriceRub { get; set; }
        public string? CommonNote { get; set; }
        public string? SpecialNote { get; set; }
        public Guid CategoryId { get; set; }
    }
}
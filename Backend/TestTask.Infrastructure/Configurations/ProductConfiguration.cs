using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Domain.Entities;

namespace TestTask.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(p => p.Name)
                   .IsUnique();

            builder.Property(p => p.Description)
                   .IsRequired()
                   .HasMaxLength(300);

            builder.Property(p => p.PriceRub)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.CommonNote)
                   .HasMaxLength(200);

            builder.Property(p => p.SpecialNote)
                   .HasMaxLength(200);

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey("CategoryId")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

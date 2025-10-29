using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Domain.Entities;

namespace TestTask.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Nickname)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.HasIndex(c => c.Nickname)
                   .IsUnique();
        }
    }
}

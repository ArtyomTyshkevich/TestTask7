using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Infrastructure.Configurations
{
    public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
    {
        public void Configure(EntityTypeBuilder<Balance> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasOne(b => b.Resource)
                .WithMany()
                .HasForeignKey("ResourceId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Unit)
                .WithMany()
                .HasForeignKey("UnitId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(b => b.Quantity).IsRequired();

            builder.HasIndex("ResourceId", "UnitId").IsUnique();
        }
    }
}

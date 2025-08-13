using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Infrastructure.Configurations
{
    public class IncomingResourceConfiguration : IEntityTypeConfiguration<IncomingResource>
    {
        public void Configure(EntityTypeBuilder<IncomingResource> builder)
        {
            builder.HasKey(ir => ir.Id);

            builder.HasOne(ir => ir.Resource)
                .WithMany()
                .HasForeignKey("ResourceId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ir => ir.Unit)
                .WithMany()
                .HasForeignKey("UnitId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ir => ir.Quantity).IsRequired();
        }
    }
}

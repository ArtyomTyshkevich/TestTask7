using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Infrastructure.Configurations
{
    public class OutgoingResourceConfiguration : IEntityTypeConfiguration<OutgoingResource>
    {
        public void Configure(EntityTypeBuilder<OutgoingResource> builder)
        {
            builder.HasKey(or => or.Id);

            builder.HasOne(or => or.Resource)
                .WithMany()
                .HasForeignKey("ResourceId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(or => or.Unit)
                .WithMany()
                .HasForeignKey("UnitId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(or => or.Quantity).IsRequired();
        }
    }
}

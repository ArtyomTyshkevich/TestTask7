using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Infrastructure.Configurations
{
    public class OutgoingDocumentConfiguration : IEntityTypeConfiguration<OutgoingDocument>
    {
        public void Configure(EntityTypeBuilder<OutgoingDocument> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Number).IsRequired();
            builder.HasIndex(d => d.Number).IsUnique();

            builder.Property(d => d.Date).IsRequired();
            builder.Property(d => d.State).IsRequired();

            builder.HasOne(d => d.Client)
                .WithMany()
                .HasForeignKey("ClientId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.OutgoingResources)
                .WithOne()
                .HasForeignKey("OutgoingDocumentId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

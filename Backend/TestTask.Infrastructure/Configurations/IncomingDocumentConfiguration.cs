using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Infrastructure.Configurations
{
    public class IncomingDocumentConfiguration : IEntityTypeConfiguration<IncomingDocument>
    {
        public void Configure(EntityTypeBuilder<IncomingDocument> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(d => d.Number).IsRequired();
            builder.HasIndex(d => d.Number).IsUnique();

            builder.Property(d => d.Date).IsRequired();

            builder.HasMany(d => d.IncomingResources)
                .WithOne()
                .HasForeignKey("IncomingDocumentId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

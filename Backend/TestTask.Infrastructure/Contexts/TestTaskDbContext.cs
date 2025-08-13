using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities.Directories;
using TestTask.Domain.Entities.Warehouse;
using TestTask.Infrastructure.Configurations;

namespace TestTask.Infrastructure.Contexts
{
    public class TestTaskDbContext : DbContext
    {
        public TestTaskDbContext(DbContextOptions<TestTaskDbContext> options)
            : base(options)
        {
            if (Database.IsRelational())
            {
                Database.Migrate();
            }
        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<IncomingDocument> IncomingDocuments { get; set; }
        public DbSet<IncomingResource> IncomingResources { get; set; }
        public DbSet<OutgoingDocument> OutgoingDocuments { get; set; }
        public DbSet<OutgoingResource> OutgoingResources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ResourceConfiguration());
            modelBuilder.ApplyConfiguration(new UnitConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new BalanceConfiguration());
            modelBuilder.ApplyConfiguration(new OutgoingDocumentConfiguration());
            modelBuilder.ApplyConfiguration(new OutgoingResourceConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }

}

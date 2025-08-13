using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Infrastructure.Contexts;

namespace TestTask.Infrastructure.DI
{
    public static class DatabaseSetup
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestTaskDbContext>(opt =>
                opt.UseLazyLoadingProxies() 
                   .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}

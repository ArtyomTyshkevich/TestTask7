using TestTask.Infrastructure.DI;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Infrastructure.Repositories.UnitOfWork;
using TestTask.Application.Interfaces.Services;
using FluentValidation.AspNetCore;
using TestTask.Application.Mappings;
using TestTask.Infrastructure.Services.Directories;
using TestTask.Infrastructure.Services.Warehouse;
using TestTask.Application.Mapping;

namespace Chat.WebAPI.DI
{
    public static class BusinessLogicRegister
    {
        public static void AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDatabase(configuration);

            // UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IBalanceService, BalanceService>();
            services.AddScoped<IIncomingDocumentService, IncomingDocumentService>();
            services.AddScoped<IIncomingResourceService, IncomingResourceService>();
            services.AddScoped<IOutgoingDocumentService, OutgoingDocumentService>();
            services.AddScoped<IOutgoingResourceService, OutgoingResourceService>();

            // AutoMapper
            services.AddAutoMapper(typeof(UnitProfile).Assembly);

            // CORS and FluentValidation
            services.ConfigureCors(configuration);
            services.AddFluentValidationAutoValidation();
            services.ConfigureDatabase(configuration);
        }
    }
}

using LocalServicesDirectory.Application.Interfaces.Repositories; 
using LocalServicesDirectory.Infrastructure.Persistence;
using LocalServicesDirectory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalServicesDirectory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LocalServicesContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IServiceRepository, ServiceRepository>(); 
            return services;
        }
    }
}


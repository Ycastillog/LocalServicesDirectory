using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LocalServicesDirectory.Infrastructure.Repositories;
using LocalServicesDirectory.Domain.Interfaces;

namespace LocalServicesDirectory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LocalServicesContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();

            return services;
        }
    }
}






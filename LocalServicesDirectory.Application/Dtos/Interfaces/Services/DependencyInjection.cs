using Microsoft.Extensions.DependencyInjection;
using LocalServicesDirectory.Application.Interfaces;
using LocalServicesDirectory.Application.Services;

namespace LocalServicesDirectory.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceService, ServiceService>();
            return services;
        }
    }
}






using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LocalServicesDirectory.Infrastructure.Persistence;

namespace LocalServicesDirectory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<LocalServicesContext>(opt =>
                opt.UseSqlServer(
                    config.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly(typeof(LocalServicesContext).Assembly.FullName)
                )
            );

            

            return services;
        }
    }
}

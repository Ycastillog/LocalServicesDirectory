using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LocalServicesDirectory.Infrastructure.Persistence
{
    public class LocalServicesContextFactory : IDesignTimeDbContextFactory<LocalServicesContext>
    {
        public LocalServicesContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            var cs = configuration.GetConnectionString("DefaultConnection")
                     ?? "Server=YEISONC\\MSSQLSERVER01;Database=LocalServicesDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";

            var options = new DbContextOptionsBuilder<LocalServicesContext>()
                .UseSqlServer(cs);

            return new LocalServicesContext(options.Options);
        }
    }
}





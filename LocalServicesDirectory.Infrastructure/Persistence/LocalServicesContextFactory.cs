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
            
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "LocalServicesDirectory.DirectoryApi");

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var conn = config.GetConnectionString("DefaultConnection");

            
            if (string.IsNullOrWhiteSpace(conn))
            {
                conn = "Server=YEISONC\\MSSQLSERVER01;Database=LocalServicesDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
            }

            var options = new DbContextOptionsBuilder<LocalServicesContext>()
                .UseSqlServer(conn, sql => sql.MigrationsAssembly(typeof(LocalServicesContext).Assembly.FullName))
                .Options;

            return new LocalServicesContext(options);
        }
    }
}




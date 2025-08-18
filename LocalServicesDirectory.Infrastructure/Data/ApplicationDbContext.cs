using Microsoft.EntityFrameworkCore;
using LocalServicesDirectory.Domain.Entities;

namespace LocalServicesDirectory.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Service> Services { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}


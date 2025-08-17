using LocalServicesDirectory.Domain.Entities;

namespace LocalServicesDirectory.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static void Seed(LocalServicesContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Plomería", Description = "Servicios de plomería" },
                    new Category { Name = "Electricidad", Description = "Servicios eléctricos" },
                    new Category { Name = "Tutoría", Description = "Clases particulares" }
                );
                context.SaveChanges();
            }
        }
    }
}


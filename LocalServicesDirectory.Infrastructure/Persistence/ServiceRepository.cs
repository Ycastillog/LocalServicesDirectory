using LocalServicesDirectory.Domain.Entities;
using LocalServicesDirectory.Domain.Interfaces;
using LocalServicesDirectory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LocalServicesDirectory.Infrastructure.Persistence.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly LocalServicesContext _db;

        public ServiceRepository(LocalServicesContext db)
        {
            _db = db;
        }

        public async Task<List<Service>> GetAllAsync()
        {
            return await _db.Services
                .Include(s => s.Category)
                .Include(s => s.City)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _db.Services
                .Include(s => s.Category)
                .Include(s => s.City)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Service> AddAsync(Service entity)
        {
            _db.Services.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Service entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var s = await _db.Services.FindAsync(id);
            if (s is null) return;
            _db.Services.Remove(s);
            await _db.SaveChangesAsync();
        }
    }
}


using LocalServicesDirectory.Domain.Entities;
using LocalServicesDirectory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalServicesDirectory.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly LocalServicesContext _db;
        public ServiceRepository(LocalServicesContext db) => _db = db;

        public async Task AddAsync(Service entity)
        {
            _db.Services.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var e = await _db.Services.FindAsync(id);
            if (e is null) return;
            _db.Services.Remove(e);
            await _db.SaveChangesAsync();
        }

        public async Task<Service?> GetByIdAsync(Guid id) =>
            await _db.Services.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IReadOnlyList<Service>> ListAsync(int skip = 0, int take = 50) =>
            await _db.Services.AsNoTracking().Skip(skip).Take(take).ToListAsync();

        public async Task UpdateAsync(Service entity)
        {
            _db.Services.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Service>> SearchAsync(string? text, Guid? categoryId, Guid? cityId, int minRating = 0, int skip = 0, int take = 50)
        {
            var q = _db.Services.AsQueryable();

            if (!string.IsNullOrWhiteSpace(text))
                q = q.Where(p => p.Name.Contains(text) || (p.Description ?? "").Contains(text));
            if (categoryId.HasValue) q = q.Where(p => p.CategoryId == categoryId.Value);
            if (cityId.HasValue) q = q.Where(p => p.CityId == cityId.Value);

            return await q.AsNoTracking().Skip(skip).Take(take).ToListAsync();
        }
    }
}

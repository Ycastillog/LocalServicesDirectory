using LocalServicesDirectory.Domain.Entities;
using LocalServicesDirectory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalServicesDirectory.Infrastructure.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly LocalServicesContext _db;
        public RatingRepository(LocalServicesContext db) => _db = db;

        public async Task AddAsync(Rating entity)
        {
            _db.Ratings.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var e = await _db.Ratings.FindAsync(id);
            if (e is null) return;
            _db.Ratings.Remove(e);
            await _db.SaveChangesAsync();
        }

        public async Task<Rating?> GetByIdAsync(Guid id) =>
            await _db.Ratings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IReadOnlyList<Rating>> ListAsync(int skip = 0, int take = 50) =>
            await _db.Ratings.AsNoTracking().Skip(skip).Take(take).ToListAsync();

        public async Task UpdateAsync(Rating entity)
        {
            _db.Ratings.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<double> GetAverageForServiceAsync(Guid serviceId)
        {
            var q = _db.Ratings.AsNoTracking().Where(r => r.ServiceId == serviceId);
            if (!await q.AnyAsync()) return 0;
            return await q.AverageAsync(r => (double)r.Score);
        }

        public async Task<IReadOnlyList<Rating>> GetForServiceAsync(Guid serviceId, int skip = 0, int take = 50) =>
            await _db.Ratings.AsNoTracking()
                .Where(r => r.ServiceId == serviceId)
                .OrderByDescending(r => r.CreatedAt)
                .Skip(skip).Take(take).ToListAsync();
    }
}


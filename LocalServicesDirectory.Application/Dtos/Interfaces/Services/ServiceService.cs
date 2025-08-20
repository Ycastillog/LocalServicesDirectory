using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LocalServicesDirectory.Application.Dtos;
using LocalServicesDirectory.Application.Interfaces;
using LocalServicesDirectory.Application.Interfaces.Repositories;
using LocalServicesDirectory.Domain.Entities;

namespace LocalServicesDirectory.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repo;

        public ServiceService(IServiceRepository repo) => _repo = repo;

        public async Task<IEnumerable<ServiceDto>> SearchAsync(string q, int skip, int take, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(q)) return Enumerable.Empty<ServiceDto>();
            take = NormalizeTake(take); skip = Math.Max(0, skip);

            var items = await _repo.SearchAsync(q.Trim(), skip, take, ct);
            return items.Select(MapToDto);
        }

        public async Task<IEnumerable<ServiceDto>> SearchAsync(Guid categoryId, Guid cityId, int minRating, int skip, int take, CancellationToken ct = default)
        {
            take = NormalizeTake(take); skip = Math.Max(0, skip);
            minRating = Math.Max(0, Math.Min(5, minRating));

            var items = await _repo.SearchAsync(categoryId, cityId, minRating, skip, take, ct);
            return items.Select(MapToDto);
        }

        public async Task<ServiceDto?> GetAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            return entity is null ? null : MapToDto(entity);
        }

        public async Task<Guid> CreateAsync(ServiceDto dto, CancellationToken ct = default)
        {
            var entity = MapToEntity(dto);
            if (entity.Id == Guid.Empty) entity.Id = Guid.NewGuid();
            await _repo.AddAsync(entity, ct);
            return entity.Id;
        }

        public async Task UpdateAsync(Guid id, ServiceDto dto, CancellationToken ct = default)
        {
            var entity = MapToEntity(dto);
            entity.Id = id;
            await _repo.UpdateAsync(entity, ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
            => await _repo.DeleteAsync(id, ct);

        private static int NormalizeTake(int take) => take <= 0 ? 50 : Math.Min(take, 200);

        private static ServiceDto MapToDto(Service s) => new ServiceDto
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description,
            CategoryId = s.CategoryId,
            CityId = s.CityId,
            AverageRating = s.AverageRating
        };

        private static Service MapToEntity(ServiceDto d) => new Service
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            CategoryId = d.CategoryId,
            CityId = d.CityId,
            AverageRating = d.AverageRating
        };
    }
}


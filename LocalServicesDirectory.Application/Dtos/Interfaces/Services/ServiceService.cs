using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalServicesDirectory.Application.Dtos;
using LocalServicesDirectory.Application.Interfaces;
using LocalServicesDirectory.Domain.Entities;
using LocalServicesDirectory.Domain.Interfaces;

namespace LocalServicesDirectory.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _services;
        private readonly IRatingRepository _ratings;

        public ServiceService(IServiceRepository services, IRatingRepository ratings)
        {
            _services = services;
            _ratings = ratings;
        }

        public async Task<Guid> CreateAsync(ServiceDto dto)
        {
            var entity = ToEntity(dto);
            await _services.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(Guid id, ServiceDto dto)
        {
            var current = await _services.GetByIdAsync(id);
            if (current is null) return;

            current.Name = dto.Name;
            current.Description = dto.Description;
            current.Phone = dto.Phone;
            current.Email = dto.Email;
            current.Address = dto.Address;
            current.CategoryId = dto.CategoryId;
            current.CityId = dto.CityId;
            current.Latitude = dto.Latitude;
            current.Longitude = dto.Longitude;
            current.WebsiteUrl = dto.WebsiteUrl;
            current.IsVerified = dto.IsVerified;
            current.UpdatedAt = DateTime.UtcNow;

            await _services.UpdateAsync(current);
        }

        public async Task DeleteAsync(Guid id) => await _services.DeleteAsync(id);

        public async Task<ServiceDto?> GetAsync(Guid id)
        {
            var e = await _services.GetByIdAsync(id);
            if (e is null) return null;
            var dto = ToDto(e);
            dto.AverageRating = await _ratings.GetAverageForServiceAsync(e.Id);
            return dto;
        }

        public async Task<IReadOnlyList<ServiceDto>> SearchAsync(string text, int skip = 0, int take = 50)
        {
            var list = await _services.SearchAsync(text, null, null, 0, skip, take);
            return await ToDtoWithAvg(list);
        }

        public async Task<IReadOnlyList<ServiceDto>> SearchAsync(Guid categoryId, Guid cityId, int minRating = 0, int skip = 0, int take = 50)
        {
            var list = await _services.SearchAsync(null, categoryId, cityId, minRating, skip, take);
            return await ToDtoWithAvg(list);
        }

        private async Task<IReadOnlyList<ServiceDto>> ToDtoWithAvg(IEnumerable<Service> entities)
        {
            var result = new List<ServiceDto>();
            foreach (var e in entities)
            {
                var dto = ToDto(e);
                dto.AverageRating = await _ratings.GetAverageForServiceAsync(e.Id);
                result.Add(dto);
            }
            return result;
        }

        private static Service ToEntity(ServiceDto dto) => new()
        {
            Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Phone = dto.Phone,
            Email = dto.Email,
            Address = dto.Address,
            CategoryId = dto.CategoryId,
            CityId = dto.CityId,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            WebsiteUrl = dto.WebsiteUrl,
            IsVerified = dto.IsVerified
        };

        private static ServiceDto ToDto(Service e) => new()
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            Phone = e.Phone,
            Email = e.Email,
            Address = e.Address,
            CategoryId = e.CategoryId,
            CityId = e.CityId,
            Latitude = e.Latitude,
            Longitude = e.Longitude,
            WebsiteUrl = e.WebsiteUrl,
            IsVerified = e.IsVerified,
            AverageRating = 0
        };
    }
}


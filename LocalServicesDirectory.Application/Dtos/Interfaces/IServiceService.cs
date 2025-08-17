using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocalServicesDirectory.Application.Dtos;

namespace LocalServicesDirectory.Application.Interfaces
{
    public interface IServiceService
    {
        Task<IReadOnlyList<ServiceDto>> SearchAsync(string text, int skip = 0, int take = 50);
        Task<IReadOnlyList<ServiceDto>> SearchAsync(Guid categoryId, Guid cityId, int minRating = 0, int skip = 0, int take = 50);
        Task<ServiceDto?> GetAsync(Guid id);
        Task<Guid> CreateAsync(ServiceDto dto);
        Task UpdateAsync(Guid id, ServiceDto dto);
        Task DeleteAsync(Guid id);
    }
}


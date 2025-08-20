namespace LocalServicesDirectory.Domain.Interfaces
{
    using LocalServicesDirectory.Domain.Entities;

    public interface IServiceRepository
    {
        Task<List<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(Guid id);
        Task<Service> AddAsync(Service entity);
        Task UpdateAsync(Service entity);
        Task DeleteAsync(Guid id);
    }
}


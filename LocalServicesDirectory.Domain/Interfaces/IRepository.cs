using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalServicesDirectory.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAsync(int skip = 0, int take = 50);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}


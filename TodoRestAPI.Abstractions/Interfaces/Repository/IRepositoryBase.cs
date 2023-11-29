using TodoRestAPI.Abstractions.Entities;

namespace TodoRestAPI.Abstractions.Interfaces.Repository
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
    }
}

using ContactApp.Domain.Entities.Base;

namespace ContactApp.Data.Repositories.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int? id);
        Task<IEnumerable<T>> GetAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

using ContactApp.Domain.Entities.Base;

namespace ContactApp.Data.Repositories.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetById(int? id);
        Task<IEnumerable<T>> Get();
    }
}

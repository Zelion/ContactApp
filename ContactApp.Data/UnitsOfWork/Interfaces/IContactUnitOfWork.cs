using ContactApp.Data.Repositories.Base;
using ContactApp.Domain.Entities.Base;

namespace ContactApp.Data.UnitsOfWork.Interfaces
{
    public interface IContactUnitOfWork : IDisposable
    {
        Task CommitAsync();
        void Rollback();

        IBaseRepository<T> BaseRepository<T>() where T : BaseEntity;
    }
}

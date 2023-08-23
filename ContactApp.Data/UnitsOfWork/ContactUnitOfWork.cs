using ContactApp.Data.Context;
using ContactApp.Data.Repositories;
using ContactApp.Data.Repositories.Base;
using ContactApp.Data.Repositories.Interfaces;
using ContactApp.Data.UnitsOfWork.Interfaces;
using ContactApp.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace ContactApp.Data.UnitsOfWork
{
    public class ContactUnitOfWork : IContactUnitOfWork
    {
        private readonly ContactAppContext _dbContext;
        private bool _disposed = false;

        public ContactUnitOfWork(ContactAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IBaseRepository<T> BaseRepository<T>() where T : BaseEntity
        {
            return new BaseRepository<T>(_dbContext);
        }

        public IContactRepository ContactRepository()
        {
            return new ContactRepository(_dbContext);
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Rollback()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _dbContext.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

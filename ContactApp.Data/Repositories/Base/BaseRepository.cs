using ContactApp.Data.Context;
using ContactApp.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace ContactApp.Data.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ContactAppContext dbContext;

        public BaseRepository(ContactAppContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync(int? id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }
    }
}

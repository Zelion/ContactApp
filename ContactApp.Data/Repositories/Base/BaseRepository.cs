using ContactApp.Data.Context;
using ContactApp.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace ContactApp.Data.Repositories.Base
{
    public class BaseRepository<E> : IBaseRepository<E> where E : BaseEntity
    {
        protected readonly ContactAppContext dbContext;

        public BaseRepository(ContactAppContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<E> GetById(int? id)
        {
            return await dbContext.Set<E>().FindAsync(id);
        }

        public async Task<IEnumerable<E>> Get()
        {
            return await dbContext.Set<E>().ToListAsync();
        }
    }
}

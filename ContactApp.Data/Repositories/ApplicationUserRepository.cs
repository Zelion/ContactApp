using ContactApp.Data.Context;
using ContactApp.Data.Repositories.Interfaces;
using ContactApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactApp.Data.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        protected readonly ContactAppContext dbContext;

        public ApplicationUserRepository(ContactAppContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ApplicationUser> GetById(int id)
        {
            return await dbContext.Set<ApplicationUser>().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

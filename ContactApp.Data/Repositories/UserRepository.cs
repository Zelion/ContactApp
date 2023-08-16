using ContactApp.Data.Context;
using ContactApp.Data.Repositories.Base;
using ContactApp.Data.Repositories.Interfaces;
using ContactApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactApp.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ContactAppContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> AuthenticateUser(string username, string password)
        {
            return await dbContext.Set<User>().FirstOrDefaultAsync(user => user.UserName == username && user.Password == password);
        }

        public Task<IEnumerable<User>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(int? id)
        {
            throw new NotImplementedException();
        }
    }
}

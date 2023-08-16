using ContactApp.Data.Repositories.Base;
using ContactApp.Domain.Entities;

namespace ContactApp.Data.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> AuthenticateUser(string username,string password);
    }
}

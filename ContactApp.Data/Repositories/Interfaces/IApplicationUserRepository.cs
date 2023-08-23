using ContactApp.Domain.Entities;

namespace ContactApp.Data.Repositories.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser> GetById(int id);
    }
}

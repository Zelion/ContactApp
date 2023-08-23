using ContactApp.Domain.Entities;

namespace ContactApp.Data.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact> GetByIdAsync(int? id, int userId);
        Task<IEnumerable<Contact>> GetAsync(int userId, string search);
        Task AddAsync(Contact contact);
        void Update(Contact contact);
        void Delete(Contact contact);
    }
}

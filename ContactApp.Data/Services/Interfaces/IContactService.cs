using ContactApp.Domain.Entities;

namespace ContactApp.Data.Services.Interfaces
{
    public interface IContactService
    {
        public Task<IEnumerable<Contact>> Get();
        Task<Contact> GetById(int? id);
    }
}

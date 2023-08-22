using ContactApp.Domain.DTOs;
using ContactApp.Domain.Entities;

namespace ContactApp.Data.Services.Interfaces
{
    public interface IContactService
    {
        public Task<IEnumerable<Contact>> Get();
        Task<Contact> GetById(int? id);
        Task AddAsync(ContactDTO contactDTO);
        Task UpdateAsync(ContactDTO contactDTO);
        Task DeleteAsync(int? id);
    }
}

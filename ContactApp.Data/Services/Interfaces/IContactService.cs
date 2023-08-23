using ContactApp.Domain.DTOs;

namespace ContactApp.Data.Services.Interfaces
{
    public interface IContactService
    {
        public Task<IEnumerable<ContactDTO>> Get(int userId);
        Task<ContactDTO> GetById(int? id, int userId);
        Task AddAsync(ContactDTO contactDTO, int userId);
        Task UpdateAsync(ContactDTO contactDTO, int userId);
        Task DeleteAsync(int? id, int userId);
    }
}

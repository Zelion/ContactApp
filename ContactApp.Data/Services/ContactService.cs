using ContactApp.Data.Repositories.Interfaces;
using ContactApp.Data.Services.Interfaces;
using ContactApp.Domain.Entities;

namespace ContactApp.Data.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<Contact>> Get()
        {
            return await _contactRepository.Get();
        }

        public async Task<Contact> GetById(int? id)
        {
            return await _contactRepository.GetById(id);
        }
    }
}

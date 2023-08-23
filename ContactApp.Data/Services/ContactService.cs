using AutoMapper;
using ContactApp.Data.Repositories.Interfaces;
using ContactApp.Data.Services.Interfaces;
using ContactApp.Data.UnitsOfWork.Interfaces;
using ContactApp.Domain.DTOs;
using ContactApp.Domain.Entities;

namespace ContactApp.Data.Services
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly IContactUnitOfWork _contactUOM;
        private readonly IApplicationUserRepository _applicationUserRepository;


        public ContactService(
            IMapper mapper,
            IContactUnitOfWork contactUOM,
            IApplicationUserRepository applicationUserRepository)
        {
            _mapper = mapper;
            _contactUOM = contactUOM;
            _applicationUserRepository = applicationUserRepository;
        }

        public async Task<IEnumerable<ContactDTO>> Get(int userId, string search)
        {
            var contacts = await _contactUOM.ContactRepository().GetAsync(userId, search);
            var contactDTOs = _mapper.Map<IEnumerable<ContactDTO>>(contacts);

            return contactDTOs;
        }

        public async Task<ContactDTO> GetById(int? id, int userId)
        {
            var contact = await _contactUOM.ContactRepository().GetByIdAsync(id, userId);
            var contactDTO = _mapper.Map<ContactDTO>(contact);
            return contactDTO;
        }

        public async Task AddAsync(ContactDTO contactDTO, int userId)
        {
            var contact = _mapper.Map<Contact>(contactDTO);

            var user = _applicationUserRepository.GetById(userId);
            if (user != null)
            {
                contact.User = user.Result;
            }

            SetDefaultValues(contact);

            await _contactUOM.ContactRepository().AddAsync(contact);
            await _contactUOM.CommitAsync();
        }

        public async Task UpdateAsync(ContactDTO contactDTO, int userId)
        {
            var contact = await _contactUOM.ContactRepository().GetByIdAsync(contactDTO.Id, userId);
            if (contact == null)
            {
                return; // TODO: add error
            }

            contactDTO.Update(contact, userId);

            _contactUOM.ContactRepository().Update(contact);
            await _contactUOM.CommitAsync();
        }

        public async Task DeleteAsync(int? id, int userId)
        {
            var contact = await _contactUOM.ContactRepository().GetByIdAsync(id, userId);
            if (contact == null)
            {
                return; // TODO: add error
            }

            _contactUOM.ContactRepository().Delete(contact);
            await _contactUOM.CommitAsync();
        }

        #region Private Methods

        private void SetDefaultValues(Contact contact)
        {
            contact.Created = DateTime.Now;
            contact.CreatedBy = contact.User?.UserName;
            contact.LastUpdate = DateTime.Now;
            contact.LastUpdateBy = contact.User?.UserName;
        }

        #endregion
    }
}

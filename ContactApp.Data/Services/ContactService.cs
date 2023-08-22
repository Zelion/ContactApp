using AutoMapper;
using ContactApp.Data.Services.Interfaces;
using ContactApp.Data.UnitsOfWork.Interfaces;
using ContactApp.Domain.DTOs;
using ContactApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ContactApp.Data.Services
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IContactUnitOfWork _contactUOM;

        private string _userId;

        public ContactService(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IContactUnitOfWork contactUOM)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

            _contactUOM = contactUOM;

            _userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<IEnumerable<Contact>> Get()
        {
            return await _contactUOM.BaseRepository<Contact>().GetAsync();
        }

        public async Task<Contact> GetById(int? id)
        {
            return await _contactUOM.BaseRepository<Contact>().GetByIdAsync(id);
        }

        public async Task AddAsync(ContactDTO contactDTO)
        {
            var contact = _mapper.Map<Contact>(contactDTO);
            SetDefaultValues(contact);

            await _contactUOM.BaseRepository<Contact>().AddAsync(contact);
            await _contactUOM.CommitAsync();
        }

        public async Task UpdateAsync(ContactDTO contactDTO)
        {
            var contact = await _contactUOM.BaseRepository<Contact>().GetByIdAsync(contactDTO.Id);
            if (contact == null)
            {
                return; // TODO: add error
            }

            contactDTO.Update(contact, _userId);

            _contactUOM.BaseRepository<Contact>().Update(contact);
            await _contactUOM.CommitAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var contact = await _contactUOM.BaseRepository<Contact>().GetByIdAsync(id);
            if (contact == null)
            {
                return; // TODO: add error
            }

            _contactUOM.BaseRepository<Contact>().Delete(contact);
            await _contactUOM.CommitAsync();
        }

        #region Private Methods

        private void SetDefaultValues(Contact contact)
        {
            contact.Created = DateTime.Now;
            contact.CreatedBy = _userId;
            contact.LastUpdate = DateTime.Now;
            contact.LastUpdateBy = _userId;
        }

        #endregion
    }
}

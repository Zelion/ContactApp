using ContactApp.Data.Context;
using ContactApp.Data.Repositories.Interfaces;
using ContactApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ContactApp.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        protected readonly ContactAppContext dbContext;

        public ContactRepository(ContactAppContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Contact> GetByIdAsync(int? id, int userId)
        {
            return await dbContext.Set<Contact>().FirstOrDefaultAsync(x => x.Id == id && x.User.Id == userId);
        }

        public async Task<IEnumerable<Contact>> GetAsync(int userId)
        {
            return await dbContext.Set<Contact>().Where(x => x.User.Id == userId).ToListAsync();
        }

        public async Task AddAsync(Contact contact)
        {
            await dbContext.Set<Contact>().AddAsync(contact);
        }

        public void Update(Contact contact)
        {
            dbContext.Set<Contact>().Update(contact);
        }

        public void Delete(Contact contact)
        {
            dbContext.Set<Contact>().Remove(contact);
        }
    }
}

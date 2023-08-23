using ContactApp.Data.Context;
using ContactApp.Data.Repositories.Interfaces;
using ContactApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Contact>> GetAsync(int userId, string search)
        {
            var query = dbContext.Set<Contact>()
                                    .Include(x => x.User)
                                .Where(x => x.UserId == userId)
                                .AsQueryable();

            AddFiltersOnQuery(ref query, search);

            return query.ToList();
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

        private void AddFiltersOnQuery(ref IQueryable<Contact> query, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.FirstName.Contains(search) ||
                                    x.LastName.Contains(search) ||
                                    x.Age.ToString().Contains(search) ||
                                    x.Address.Contains(search) ||
                                    x.Phone.Contains(search));
            }
        }
    }
}

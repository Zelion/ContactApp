using ContactApp.Data.Context;
using ContactApp.Data.Repositories.Base;
using ContactApp.Data.Repositories.Interfaces;
using ContactApp.Domain.Entities;

namespace ContactApp.Data.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(ContactAppContext dbContext) : base(dbContext)
        {
        }
    }
}

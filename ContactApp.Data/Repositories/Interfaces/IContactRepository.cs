using ContactApp.Data.Repositories.Base;
using ContactApp.Domain.Entities;

namespace ContactApp.Data.Repositories.Interfaces
{
    public interface IContactRepository : IBaseRepository<Contact>
    {
    }
}

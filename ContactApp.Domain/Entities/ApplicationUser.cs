using Microsoft.AspNetCore.Identity;

namespace ContactApp.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public IEnumerable<Contact> Contacts { get; set; }
    }
}

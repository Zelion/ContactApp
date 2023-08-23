using ContactApp.Domain.DTOs;

namespace ContactApp.Models
{
    public class ContactViewModel
    {
        public IEnumerable<ContactDTO> Contacts { get; set; }
        public string Search { get; set; }
    }
}

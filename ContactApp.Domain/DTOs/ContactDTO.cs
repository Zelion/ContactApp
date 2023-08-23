using ContactApp.Domain.Entities;

namespace ContactApp.Domain.DTOs
{
    public class ContactDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public void Update(Contact contact, int userId)
        {
            contact.FirstName = FirstName;
            contact.LastName = LastName;
            contact.Age = Age;
            contact.Address = Address;
            contact.Phone = Phone;
            contact.UserId = userId;
            contact.LastUpdate = DateTime.Now;
            contact.LastUpdateBy = contact.User.UserName;
        }
    }
}

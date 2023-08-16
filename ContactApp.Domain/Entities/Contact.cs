using ContactApp.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace ContactApp.Domain.Entities
{
    public class Contact : BaseEntity
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public int Age { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }
    }
}

using ContactApp.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactApp.Domain.Entities
{
    public class Contact : BaseEntity
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = default!;

        [MaxLength(50)]
        public string LastName { get; set; } = default!;

        public int Age { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}

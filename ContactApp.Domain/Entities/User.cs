using ContactApp.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace ContactApp.Domain.Entities
{
    public class User : BaseEntity
    {
        [MaxLength(20)]
        [Required(ErrorMessage ="Please enter username")]
        [Display(Name = "Please enter username")]
        public string UserName { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter password")]
        [Display(Name = "Please enter password")]
        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}

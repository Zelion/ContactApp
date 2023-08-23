using System.ComponentModel.DataAnnotations;

namespace ContactApp.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

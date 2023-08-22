using System.ComponentModel.DataAnnotations;

namespace ContactApp.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}

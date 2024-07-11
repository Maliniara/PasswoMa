using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class PasswordEntry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

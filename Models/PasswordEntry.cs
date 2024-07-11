using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class PasswordEntry
    {
        [Key]
        public int Id { get; set; }
        public string URL { get; set; }
        public string User { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

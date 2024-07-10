using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class PasswordEntry
    {
        [Key]
        public int Id { get; set; }
        public string URL { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}

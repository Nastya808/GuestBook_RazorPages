using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuestBookApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Pwd { get; set; } = string.Empty;

        [NotMapped]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

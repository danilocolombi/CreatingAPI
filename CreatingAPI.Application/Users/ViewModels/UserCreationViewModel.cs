using System.ComponentModel.DataAnnotations;

namespace CreatingAPI.Application.Users.ViewModels
{
    public class UserCreationViewModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Name { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Password { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(150)]
        public string Email { get; set; }
    }
}

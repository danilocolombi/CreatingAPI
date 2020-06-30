using System.ComponentModel.DataAnnotations;

namespace CreatingAPI.Application.Users.ViewModels
{
    public class UserCreationViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        [MinLength(3, ErrorMessage = "The {0} can't have less than 3 characters")]
        [MaxLength(150, ErrorMessage = "The {0} can't have more than 150 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MinLength(5, ErrorMessage = "The {0} can't have less than 5 characters")]
        [MaxLength(50, ErrorMessage = "The {0} can't have more than 50 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MinLength(8, ErrorMessage = "The {0} can't have less than 8 characters")]
        [MaxLength(150, ErrorMessage = "The {0} can't have more than 150 characters")]
        public string Email { get; set; }
    }
}

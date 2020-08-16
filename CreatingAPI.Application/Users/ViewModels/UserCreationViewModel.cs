using System.ComponentModel.DataAnnotations;

namespace CreatingAPI.Application.Users.ViewModels
{
    public class UserCreationViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "The {0} needs to have between 3 and 150 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "The {0} needs to have between 5 and 50 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} is required")]    
        [StringLength(maximumLength: 150, MinimumLength = 8, ErrorMessage = "The {0} needs to have between 8 and 150 characters")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CreatingAPI.Application.Unscrumbles.ViewModels
{
    public class ExerciseViewModel
    {
        [Required(ErrorMessage = "The {0} is required")]
        [MinLength(3, ErrorMessage = "The {0} can't have less than 3 characters")]
        [MaxLength(150, ErrorMessage = "The {0} can't have more than 150 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [Range(0, 50, ErrorMessage = "Invalid {0}")]
        public int Position { get; set; }
    }
}

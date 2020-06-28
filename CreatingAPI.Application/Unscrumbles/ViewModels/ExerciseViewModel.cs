using System.ComponentModel.DataAnnotations;

namespace CreatingAPI.Application.Unscrumbles.ViewModels
{
    public class ExerciseViewModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        [Range(0, 50)]
        public int Position { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreatingAPI.Application.Unscrumbles.ViewModels
{
    public class UnscrumbleCreationViewModel
    {
        [MinLength(3, ErrorMessage = "The {0} can't have less than 3 characters")]
        [MaxLength(150, ErrorMessage = "The {0} can't have more than 150 characters")]
        [Required(ErrorMessage = "{0} is required")]
        public string Title { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Invalid {0}")]
        [Required(ErrorMessage = "{0} is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public bool IsPublic { get; set; }

        public IEnumerable<ExerciseViewModel> Exercises { get; set; }
    }
}

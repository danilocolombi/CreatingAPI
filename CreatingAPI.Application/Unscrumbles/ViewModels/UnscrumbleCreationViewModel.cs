using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CreatingAPI.Application.Unscrumbles.ViewModels
{
    public class UnscrumbleCreationViewModel
    {
        [MinLength(3)]
        [MaxLength(150)]
        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required]
        [DisplayName("User Id")]
        public int UserId { get; set; }

        [Required]
        [DisplayName("Is Public")]
        public bool IsPublic { get; set; }

        public IEnumerable<ExerciseViewModel> Exercises { get; set; }
    }
}

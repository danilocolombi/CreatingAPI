using System;
using System.ComponentModel.DataAnnotations;

namespace CreatingAPI.Application.Games.ViewModels
{
    public class GameCreationViewModel
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int UserId { get; private set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int UnscrumbleId { get; private set; }

        [Required]
        public DateTime StartedAt { get; private set; }

        [Required]
        public DateTime EndedAt { get; private set; }

        [Required]
        [Range(0, 50)]
        public int NumberOfCorrectAnswers { get; private set; }

        [Required]
        [Range(0, 50)]
        public int NumberOfWrongAnswers { get; private set; }
    }
}

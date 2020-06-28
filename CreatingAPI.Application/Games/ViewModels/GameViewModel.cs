using System;

namespace CreatingAPI.Application.Games.ViewModels
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public int UserId { get; private set; }
        public int UnscrumbleId { get; private set; }
        public DateTime StartedAt { get; private set; }
        public DateTime EndedAt { get; private set; }
        public int NumberOfCorrectAnswers { get; private set; }
        public int NumberOfWrongAnswers { get; private set; }
    }
}

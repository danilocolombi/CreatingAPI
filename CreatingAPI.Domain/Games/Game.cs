using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Unscrambles;
using CreatingAPI.Domain.Users;
using System;

namespace CreatingAPI.Domain.Games
{
    public class Game : Entity
    {
        public DateTime StartedAt { get; private set; }
        public DateTime EndedAt { get; private set; }
        public int NumberOfCorrectAnswers { get;}
        public int NumberOfWrongAnswers { get; }
        public int UserId { get; }
        public virtual User User { get; }
        public int UnscrambleId { get;}
        public virtual Unscramble Unscramble { get; }

        public Game() { }

        public Game(int userId, int activityId, DateTime startedAt, DateTime endedAt, int numberOfCorrectAnswers, int numberOfWrongAnswers)
        {
            UserId = userId;
            UnscrambleId = activityId;
            SetDates(startedAt, endedAt);
            NumberOfCorrectAnswers = numberOfCorrectAnswers;
            NumberOfWrongAnswers = numberOfWrongAnswers;
        }

        public bool SetDates(DateTime startedAt, DateTime endedAt)
        {
            if (startedAt == null || startedAt == default)
            {
                ValidationErrors.Add(new ValidationError("The started date is required"));
                return false;
            }
            if (endedAt == null || endedAt == default)
            {
                ValidationErrors.Add(new ValidationError("The ended date is required"));
                return false;
            }
            if (startedAt > endedAt)
            {
                ValidationErrors.Add(new ValidationError("The started date can't be after the ended date"));
                return false;
            }

            StartedAt = startedAt;
            EndedAt = endedAt;

            return true;
        }

        public bool ValidateNumberOfAnswers(int numberOfQuestions)
        {
            var totalNumberOfAnswers = NumberOfCorrectAnswers + NumberOfWrongAnswers;

            return totalNumberOfAnswers == numberOfQuestions ? true : false;
        }

        public override string ToString()
             => $"[Id: {Id}; StartedAt: {StartedAt}; EndedAt: {EndedAt}; UnscrumbleId: {UnscrambleId}; UserId: {UserId}]";

        public override int GetHashCode()
            => (StartedAt, EndedAt, NumberOfCorrectAnswers, NumberOfWrongAnswers, UnscrambleId, UserId).GetHashCode();

        public override bool Equals(object obj)
        {
            var otherGame = obj as Game;

            if (otherGame == null)
                return false;

            if (this.StartedAt == otherGame.StartedAt &&
                this.EndedAt == otherGame.EndedAt &&
                this.NumberOfCorrectAnswers == otherGame.NumberOfCorrectAnswers &&
                this.NumberOfWrongAnswers == otherGame.NumberOfWrongAnswers &&
                this.UnscrambleId == otherGame.UnscrambleId &&
                this.UserId == otherGame.UserId)
                return true;

            return false;
        }

        public static bool operator ==(Game game1, Game game2)
            => ReferenceEquals(game1, null) ? ReferenceEquals(game2, null) : game1.Equals(game2);

        public static bool operator !=(Game game1, Game game2)
            => !(game1 == game2);
    }
}
using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Unscrumbles;
using CreatingAPI.Domain.Users;
using Microsoft.VisualBasic.CompilerServices;
using System;

namespace CreatingAPI.Domain.Games
{
    public class Game : Entity
    {
        public DateTime StartedAt { get; private set; }
        public DateTime EndedAt { get; private set; }
        public int NumberOfCorrectAnswers { get; private set; }
        public int NumberOfWrongAnswers { get; private set; }
        public int UserId { get; private set; }
        public virtual User User { get; set; }
        public int UnscrumbleId { get; private set; }
        public virtual Unscrumble Unscrumble { get; set; }

        public Game() { }

        public Game(int userId, int activityId, DateTime startedAt, DateTime endedAt, int numberOfCorrectAnswers, int numberOfWrongAnwers)
        {
            SetUserId(userId);
            SetUnscrumbleId(activityId);
            SetDates(startedAt, endedAt);
            SetNumberOfCorrectAnswers(numberOfCorrectAnswers);
            SetNumberOfWrongAnswers(numberOfWrongAnwers);
        }

        public bool SetUserId(int userId)
        {
            if (userId <= 0)
            {
                ValidationErrors.Add(new ValidationError("The user is invalid"));
                return false;
            }

            UserId = userId;

            return true;
        }

        public bool SetUnscrumbleId(int unscrumbleId)
        {
            if (unscrumbleId <= 0)
            {
                ValidationErrors.Add(new ValidationError("The activity is invalid"));
                return false;
            }

            UnscrumbleId = unscrumbleId;

            return true;
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

        public bool SetNumberOfCorrectAnswers(int numberOfCorrectAnswers)
        {
            if (numberOfCorrectAnswers < 0)
            {
                ValidationErrors.Add(new ValidationError("The number of correct answers can't be less than 0"));
                return false;
            }

            NumberOfCorrectAnswers = numberOfCorrectAnswers;

            return true;
        }

        public bool SetNumberOfWrongAnswers(int numberOfWrongAnswers)
        {
            if (numberOfWrongAnswers < 0)
            {
                ValidationErrors.Add(new ValidationError("The number of wrong answers can't be less than 0"));
                return false;
            }

            NumberOfWrongAnswers = numberOfWrongAnswers;

            return true;
        }

        public bool ValidateNumberOfAnswers(int numberOfQuestions)
        {
            var totalNumberOfAnswers = NumberOfCorrectAnswers + NumberOfWrongAnswers;

            return totalNumberOfAnswers == numberOfQuestions ? true : false;
        }

        public override string ToString()
             => $"[Id: {Id}; StartedAt: {StartedAt}; EndedAt: {EndedAt}; UnscrumbleId: {UnscrumbleId}; UserId: {UserId}]";

        public override int GetHashCode()
            => (StartedAt, EndedAt, NumberOfCorrectAnswers, NumberOfWrongAnswers, UnscrumbleId, UserId).GetHashCode();

        public override bool Equals(object obj)
        {
            var otherGame = obj as Game;

            if (otherGame == null)
                return false;

            if (this.StartedAt == otherGame.StartedAt &&
                this.EndedAt == otherGame.EndedAt &&
                this.NumberOfCorrectAnswers == otherGame.NumberOfCorrectAnswers &&
                this.NumberOfWrongAnswers == otherGame.NumberOfWrongAnswers &&
                this.UnscrumbleId == otherGame.UnscrumbleId &&
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
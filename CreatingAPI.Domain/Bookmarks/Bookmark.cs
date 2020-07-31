using CreatingAPI.Domain.Activities;
using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Pickers;
using CreatingAPI.Domain.Quizzes;
using CreatingAPI.Domain.TicTacToes;
using CreatingAPI.Domain.Unscrambles;
using CreatingAPI.Domain.Users;

namespace CreatingAPI.Domain.Bookmarks
{
    public class Bookmark : Entity
    {
        public int UserId { get; }
        public virtual User User { get; }
        public int? UnscrambleId { get; private set; }
        public virtual Unscramble Unscramble { get; }
        public int? TicTacToeId { get; private set; }
        public virtual TicTacToe TicTacToe { get; }
        public int? PickerId { get; private set; }
        public virtual Picker Picker { get; }
        public int? QuizId { get; private set; }
        public virtual Quiz Quiz { get; }

        public Bookmark() { }
        public Bookmark(int userId, int activityId, KindOfActivity kindOfActivity)
        {
            UserId = userId;
            SetId(activityId, kindOfActivity);
        }

        public bool SetId(int activityId, KindOfActivity kindOfActivity)
        {
            if (activityId <= 0)
            {
                ValidationErrors.Add(new ValidationError("The activity is invalid"));
                return false;
            }

            switch (kindOfActivity)
            {
                case KindOfActivity.Unscramble:
                    UnscrambleId = activityId;
                    break;
                case KindOfActivity.TicTacToe:
                    TicTacToeId = activityId;
                    break;
                case KindOfActivity.Picker:
                    PickerId = activityId;
                    break;
                case KindOfActivity.Quiz:
                    QuizId = activityId;
                    break;
                default:
                    ValidationErrors.Add(new ValidationError("The kind of activity is invalid"));
                    return false;
            }

            return true;
        }

        public override string ToString()
        => $"[Id: {Id}; UserId: {UserId}; UnscrumbleId: {UnscrambleId}; TicTacToeId: {TicTacToeId}; PickerId: {PickerId}]";

        public override int GetHashCode()
        => (UserId, UnscrambleId.GetValueOrDefault(), TicTacToeId.GetValueOrDefault(), PickerId.GetValueOrDefault()).GetHashCode();

        public override bool Equals(object obj)
        {
            var otherBookmark = obj as Bookmark;

            if (otherBookmark == null)
                return false;

            if (this.UnscrambleId.GetValueOrDefault() == otherBookmark.UnscrambleId.GetValueOrDefault() &&
                this.TicTacToeId.GetValueOrDefault() == otherBookmark.TicTacToeId.GetValueOrDefault() &&
                this.PickerId.GetValueOrDefault() == otherBookmark.PickerId.GetValueOrDefault() &&
                this.UserId == otherBookmark.UserId)
                return true;

            return false;
        }

        public static bool operator ==(Bookmark bookmark1, Bookmark bookmark2)
            => ReferenceEquals(bookmark1, null) ? ReferenceEquals(bookmark2, null) : bookmark1.Equals(bookmark2);

        public static bool operator !=(Bookmark bookmark1, Bookmark bookmark2)
            => !(bookmark1 == bookmark2);
    }
}
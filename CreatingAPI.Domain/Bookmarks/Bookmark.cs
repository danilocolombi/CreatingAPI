using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Unscrumbles;
using CreatingAPI.Domain.Users;

namespace CreatingAPI.Domain.Bookmarks
{
    public class Bookmark : Entity
    {
        public int UserId { get; private set; }
        public virtual User User { get; set; }
        public int UnscrumbleId { get; private set; }
        public virtual Unscrumble Unscrumble { get; set; }

        public Bookmark() { }
        public Bookmark(int userId, int activityId)
        {
            SetUserId(userId);
            SetUnscrumbleId(activityId);
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

        public override string ToString()
        => $"[Id: {Id}; UserId: {UserId}; UnscrumbleId: {UnscrumbleId}]";

        public override int GetHashCode()
        => (UserId, UnscrumbleId).GetHashCode();

        public override bool Equals(object obj)
        {
            var otherBookmark = obj as Bookmark;

            if (otherBookmark == null)
                return false;

            if (this.UnscrumbleId == otherBookmark.UnscrumbleId &&
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
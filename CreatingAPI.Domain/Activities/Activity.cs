using CreatingAPI.Domain.Bookmarks;
using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Users;
using System;
using System.Collections.Generic;

namespace CreatingAPI.Domain.Activities
{
    public class Activity : Entity
    {
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPublic { get; private set; }
        public int UserId { get;}
        public virtual User User { get; set; }
        public virtual ICollection<Bookmark> Bookmarks { get; set; }

        public Activity() { }

        public Activity(string title, int userId, bool isPublic)
        {
            Title = title;
            UserId = userId;
            IsPublic = isPublic;
            CreatedAt = DateTime.Now;
        }

        public override string ToString()
            => $"[Id: {Id}; Title: {Title}; CreatedAt: {CreatedAt}]";

        public override int GetHashCode()
            => (Title, CreatedAt, IsPublic, UserId).GetHashCode();

        public override bool Equals(object obj)
        {
            var otherActivity = obj as Activity;

            if (otherActivity == null) return false;

            if (string.Equals(this.Title, otherActivity.Title) &&
                this.CreatedAt == otherActivity.CreatedAt &&
                this.IsPublic == otherActivity.IsPublic &&
                this.UserId == otherActivity.UserId)
                return true;

            return false;
        }

        public static bool operator ==(Activity activity1, Activity activity2)
            => ReferenceEquals(activity1, null) ? ReferenceEquals(activity2, null) : activity1.Equals(activity2);

        public static bool operator !=(Activity activity1, Activity activity2)
          => !(activity1 == activity2);
    }
}

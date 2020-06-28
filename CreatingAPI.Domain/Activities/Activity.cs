using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Users;
using System;

namespace CreatingAPI.Domain.Activities
{
    public class Activity : Entity
    {
        public string Title { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsPublic { get; private set; }
        public int UserId { get; private set; }
        public virtual User User { get; private set; }

        public Activity() { }

        public Activity(string title, int userId, bool isPublic)
        {
            SetTitle(title);
            SetUserId(userId);
            SetIsPublic(isPublic);
            SetCreatedAt(DateTime.Now);
        }
        public bool SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                ValidationErrors.Add(new ValidationError("The title can't be empty"));
                return false;
            }
            if (title.Length < 3)
            {
                ValidationErrors.Add(new ValidationError("The title can't have less than 3 characters"));
                return false;
            }
            if (title.Length > 150)
            {
                ValidationErrors.Add(new ValidationError("The title can't have more than 150 characters"));
                return false;
            }

            Title = title;

            return true;
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

        public void SetIsPublic(bool isPublic)
        {
            IsPublic = isPublic;
        }

        private bool SetCreatedAt(DateTime createdAt)
        {
            if (createdAt == null || createdAt == default)
            {
                ValidationErrors.Add(new ValidationError("The creation date is required"));
                return false;
            }
            if (createdAt > DateTime.Now)
            {
                ValidationErrors.Add(new ValidationError("The creation date can't be in the future"));
                return false;
            }

            CreatedAt = createdAt;

            return true;
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

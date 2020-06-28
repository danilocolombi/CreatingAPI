using CreatingAPI.Domain.Activities;
using CreatingAPI.Domain.Bookmarks;
using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Linq;

namespace CreatingAPI.Domain.Unscrumbles
{
    public class Unscrumble : Activity
    {
        public virtual List<Exercise> Exercises { get; private set; } = new List<Exercise>();
        public virtual ICollection<Bookmark> Bookmarks { get; private set; }
        Unscrumble() { }

        public Unscrumble(string title, int userId, bool isPublic) : base(title, userId, isPublic)
        {
        }


        public bool AddExercises(IEnumerable<Exercise> exercises)
        {
            Exercises.Clear();

            foreach (var exercise in exercises)
            {
                if (!exercise.IsValid())
                {
                    ValidationErrors.Add(new ValidationError($"Exercise {exercise.Position} is invalid: {exercise.ValidationErrors.FirstOrDefault()}"));
                    return false;
                }
            }

            Exercises.AddRange(exercises);

            if (Id > 0)
                Exercises.ForEach(e => e.UnscrumbleId = Id);

            return true;
        }

        public override string ToString()
            => base.ToString();

        public override int GetHashCode()
            => base.GetHashCode();

        public override bool Equals(object obj)
        {
            var otherUnscrumble = obj as Unscrumble;

            if (otherUnscrumble == null) return false;

            if (string.Equals(this.Title, otherUnscrumble.Title) &&
                this.CreatedAt == otherUnscrumble.CreatedAt &&
                this.IsPublic == otherUnscrumble.IsPublic &&
                this.UserId == otherUnscrumble.UserId)
                return true;

            return false;
        }

        public static bool operator ==(Unscrumble unscrumble1, Unscrumble unscrumble2)
            => ReferenceEquals(unscrumble1, null) ? ReferenceEquals(unscrumble2, null) : unscrumble1.Equals(unscrumble2);

        public static bool operator !=(Unscrumble unscrumble1, Unscrumble unscrumble2)
          => !(unscrumble1 == unscrumble2);
    }
}
using CreatingAPI.Domain.Activities;
using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Linq;

namespace CreatingAPI.Domain.Unscrambles
{
    public class Unscramble : Activity
    {
        public virtual ICollection<Exercise> Exercises { get; private set; } = new List<Exercise>();
        Unscramble() { }

        public Unscramble(string title, int userId, bool isPublic) : base(title, userId, isPublic)
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
                    Exercises.Clear();
                    return false;
                }

                Exercises.Add(exercise);
            }

            if (Id > 0)
                Exercises.ToList().ForEach(e => e.UnscrambleId = Id);

            return true;
        }

        public override string ToString()
            => base.ToString();

        public override int GetHashCode()
            => base.GetHashCode();

        public override bool Equals(object obj)
        {
            var otherUnscramble = obj as Unscramble;

            if (otherUnscramble == null) return false;

            if (string.Equals(this.Title, otherUnscramble.Title) &&
                this.CreatedAt == otherUnscramble.CreatedAt &&
                this.IsPublic == otherUnscramble.IsPublic &&
                this.UserId == otherUnscramble.UserId)
                return true;

            return false;
        }

        public static bool operator ==(Unscramble unscramble1, Unscramble unscramble2)
            => ReferenceEquals(unscramble1, null) ? ReferenceEquals(unscramble2, null) : unscramble1.Equals(unscramble2);

        public static bool operator !=(Unscramble unscramble1, Unscramble unscramble2)
          => !(unscramble1 == unscramble2);
    }
}
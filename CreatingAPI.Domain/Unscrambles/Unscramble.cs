using CreatingAPI.Domain.Activities;
using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Core.Extensions;
using CreatingAPI.Domain.Unscrambles.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace CreatingAPI.Domain.Unscrambles
{
    public class Unscramble : Activity
    {
        public virtual ICollection<Exercise> Exercises { get; private set; }
        Unscramble() { }

        public Unscramble(string title, int userId, bool isPublic) : base(title, userId, isPublic)
        {
        }

        public bool AddExercises(IEnumerable<Exercise> exercises)
        {
            Exercises = new List<Exercise>(exercises.Count());

            foreach (var exercise in exercises)
            {
                if (!exercise.IsValid())
                {
                    ValidationErrors.Add(new ValidationError($"Exercise {exercise.Position} is invalid: {exercise.ValidationErrors.FirstOrDefault().Message}"));
                    return false;
                }

                Exercises.Add(exercise);
            }

            if (Id > 0)
                Exercises.ToList().ForEach(e => e.UnscrambleId = Id);

            return true;
        }

        public IEnumerable<ShuffledExercise> GetShuffledExercises(bool randomizeExercises)
        {
            var shuffledExercises = new List<ShuffledExercise>();

            foreach (var exercise in Exercises)
            {
                var words = exercise.Description.Split(' ');

                (string value, int position) shortestWord;

                while (words.Length > 4)
                {
                    shortestWord = GetShortestWord(words);

                    words = words.Where(word => string.Compare(word, shortestWord.value) != 0).ToArray();

                    if (shortestWord.position == 0)
                        words[0] = words[0].Insert(0, $"{shortestWord.value} ");

                    else if (shortestWord.position == 1)
                        words[0] += $" {shortestWord.value}";

                    else if (shortestWord.position == 2)
                        words[1] += $" {shortestWord.value}";

                    else if (shortestWord.position == 3)
                        words[2] += $" {shortestWord.value}";

                    else if (shortestWord.position == 4)
                        words[3] += $" {shortestWord.value}";

                    else if (shortestWord.position == 5)
                        words[4] += $" {shortestWord.value}";

                    else if (shortestWord.position == 6)
                        words[5] += $" {shortestWord.value}";

                    else if (shortestWord.position == 7)
                        words[6] += $" {shortestWord.value}";

                    else if (shortestWord.position == 8)
                        words[7] += $" {shortestWord.value}";

                    else if (shortestWord.position == 9)
                        words[8] += $" {shortestWord.value}";
                }

                words.Shuffle();
                shuffledExercises.Add(new ShuffledExercise(words[0], words[1], words[2], words[3], exercise.Description));
            }

            if (randomizeExercises)
                shuffledExercises.Shuffle();

            return shuffledExercises;
        }

        private (string value, int position) GetShortestWord(string[] words)
        {
            string minValue = words[0];
            int positionMinValue = 0;

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length < minValue.Length)
                {
                    minValue = words[i];
                    positionMinValue = i;
                }
            }

            return (minValue, positionMinValue);
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
using CreatingAPI.Domain.Core;

namespace CreatingAPI.Domain.Unscrambles
{
    public class Exercise : Entity
    {
        public string Description { get; private set; }
        public int Position { get; set; }
        public int UnscrambleId { get; set; }
        public virtual Unscramble Unscramble { get; set; }

        private const int MIN_NUMBER_OF_WORDS = 3;
        private const int MAX_NUMBER_OF_WORDS = 10;
        private const int MIN_POSITION_EXERCISE = 1;
        private const int MAX_POSITION_EXERCISE = 50;

        public Exercise() { }

        public Exercise(string description, int position)
        {
            SetDescription(description);
            SetPosition(position);
        }

        public bool SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                ValidationErrors.Add(new ValidationError("The description can't be empty"));
                return false;
            }
            if (description.Length < 3)
            {
                ValidationErrors.Add(new ValidationError("The description can't have less than 3 characters"));
                return false;
            }
            if (description.Length > 150)
            {
                ValidationErrors.Add(new ValidationError("The description can't have more than 150 characters"));
                return false;
            }

            var words = description.Split(' ');

            if (words.Length < MIN_NUMBER_OF_WORDS)
            {
                ValidationErrors.Add(new ValidationError($"The mininum number of words to create an exercise is {MIN_NUMBER_OF_WORDS}"));
                return false;
            }
            if (words.Length > MAX_NUMBER_OF_WORDS)
            {
                ValidationErrors.Add(new ValidationError($"The maximum number of words to create an exercise is {MAX_NUMBER_OF_WORDS}"));
                return false;
            }

            Description = description;
            return true;
        }

        public bool SetPosition(int position)
        {
            if (position < MIN_POSITION_EXERCISE)
            {
                ValidationErrors.Add(new ValidationError($"The position can't be less than {MAX_POSITION_EXERCISE}"));
                return false;
            }
            if (position > MAX_POSITION_EXERCISE)
            {
                ValidationErrors.Add(new ValidationError($"The position can't be more than {MAX_POSITION_EXERCISE}"));
                return false;
            }

            Position = position;
            return true;
        }

        public override string ToString()
            => $"[Id: {Id}; Description: {Description}; Position: {Position}]";

        public override int GetHashCode()
            => (Id, Description, Position, UnscrambleId).GetHashCode();
    }
}
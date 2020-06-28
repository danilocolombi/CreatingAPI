using CreatingAPI.Domain.Core;

namespace CreatingAPI.Domain.Unscrumbles
{
    public class Exercise : Entity
    {
        public string Description { get; private set; }
        public int Position { get; set; }
        public int UnscrumbleId { get; set; }
        public virtual Unscrumble Unscrumble { get; set; }

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

            Description = description;
            return true;
        }

        public bool SetPosition(int position)
        {
            if (position < 0)
            {
                ValidationErrors.Add(new ValidationError("The position can't be less than 0"));
                return false;
            }

            Position = position;
            return true;
        }

        public override string ToString()
            => $"[Id: {Id}; Description: {Description}; Position: {Position}]";

        public override int GetHashCode()
            => (Id, Description, Position, UnscrumbleId).GetHashCode();
    }
}
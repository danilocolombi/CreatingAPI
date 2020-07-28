using CreatingAPI.Domain.Core;

namespace CreatingAPI.Domain.Pickers
{
    public class PickerTopic : Entity
    {
        public int Description { get; private set; }
        public int PickerId { get; set; }
        public virtual Picker Picker { get; set; }

        public PickerTopic() { }

        public PickerTopic(string description)
        {
            SetDescription(description);
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

            return true;
        }
    }
}
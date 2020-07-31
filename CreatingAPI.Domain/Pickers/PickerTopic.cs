using CreatingAPI.Domain.Core;

namespace CreatingAPI.Domain.Pickers
{
    public class PickerTopic : Entity
    {
        public string Description { get; }
        public int PickerId { get; private set; }
        public virtual Picker Picker { get; }

        public PickerTopic() { }

        public PickerTopic(string description)
        {
            Description = description;
        }

        public void SetPickerId(int pickerId)
        {
            PickerId = pickerId;
        }
    }
}
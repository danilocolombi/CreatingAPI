using CreatingAPI.Domain.Activities;
using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Linq;

namespace CreatingAPI.Domain.Pickers
{
    public class Picker : Activity
    {
        public virtual ICollection<PickerTopic> Topics { get; private set; }

        public Picker() { }

        public Picker(string title, int userId, bool isPublic) : base(title, userId, isPublic)
        {

        }

        public bool AddTopics(IEnumerable<PickerTopic> topics)
        {
            Topics = new List<PickerTopic>(topics.Count());

            foreach (var topic in topics)
            {
                if (!topic.IsValid())
                {
                    ValidationErrors.Add(new ValidationError($"{topic.ValidationErrors.FirstOrDefault().Message}"));
                    return false;
                }

                if (Id > 0)
                    topic.PickerId = Id;

                Topics.Add(topic);
            }

            return true;
        }

        public override string ToString()
        => base.ToString();

        public override int GetHashCode()
            => base.GetHashCode();

        public override bool Equals(object obj)
        {
            var otherPicker = obj as Picker;

            if (otherPicker == null) return false;

            if (string.Equals(this.Title, otherPicker.Title) &&
                this.CreatedAt == otherPicker.CreatedAt &&
                this.IsPublic == otherPicker.IsPublic &&
                this.UserId == otherPicker.UserId)
                return true;

            return false;
        }

        public static bool operator ==(Picker picker1, Picker picker2)
            => ReferenceEquals(picker1, null) ? ReferenceEquals(picker2, null) : picker1.Equals(picker2);

        public static bool operator !=(Picker picker1, Picker picker2)
          => !(picker1 == picker2);
    }
}

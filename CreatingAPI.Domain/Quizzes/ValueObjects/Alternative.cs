using System;
using System.Diagnostics.CodeAnalysis;

namespace CreatingAPI.Domain.Quizzes.ValueObjects
{
    public class Alternative : IEquatable<Alternative>
    {
        public string Description { get; private set; }
        public bool IsCorrect { get; private set; }

        private const int DESCRIPTION_MAX_LENGTH = 150;
        private const int DESCRIPTION_MIN_LENGTH = 1;

        private Alternative(string description, bool isCorrect)
        {
            Description = description;
            IsCorrect = isCorrect;
        }

        public static Alternative Parse(string description, bool isCorrect)
        {
            if (TryParse(description, isCorrect, out var alternative))
                return alternative;

            throw new Exception("Invalid alternative");
        }

        public static bool TryParse(string description, bool isCorrect, out Alternative alternative)
        {
            if (string.IsNullOrWhiteSpace(description) ||
                description.Length < DESCRIPTION_MIN_LENGTH ||
                description.Length > DESCRIPTION_MAX_LENGTH)
            {
                alternative = null;
                return false;
            }

            alternative = new Alternative(description, isCorrect);
            return true;
        }

        public override string ToString()
       => $"[Description: {Description}; IsCorrect: {IsCorrect}]";

        public override int GetHashCode()
          => (Description, IsCorrect).GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is Alternative otherAlternative)
                return Equals(otherAlternative);

            return false;
        }

        public bool Equals([AllowNull] Alternative other)
        {
            if (other == null)
                return false;

            return this.Description == other.Description &&
                  this.IsCorrect == other.IsCorrect;
        }

        public static bool operator ==(Alternative alternative1, Alternative alternative2)
            => ReferenceEquals(alternative1, null) ? ReferenceEquals(alternative2, null) : alternative1.Equals(alternative2);

        public static bool operator !=(Alternative alternative1, Alternative alternative2)
            => !(alternative1 == alternative2);
    }
}

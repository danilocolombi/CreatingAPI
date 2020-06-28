using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CreatingAPI.Domain.Core.ValueObjects
{
    public class Password : IEquatable<Password>
    {
        public string Characters { get; private set; }
        private const int minLength = 5;
        private const int maxLength = 50;
        public ICollection<ValidationError> ValidationErrors = new List<ValidationError>();

        public bool IsValid(string password)
        {
            if (password.Length < minLength) ValidationErrors.Add(new ValidationError($"Your password needs to have at least {minLength} characters"));
            if (password.Length > maxLength) ValidationErrors.Add(new ValidationError($"Your password can't have more than {maxLength} characters"));
            if (!password.Any(char.IsUpper)) ValidationErrors.Add(new ValidationError("Your password needs to have at least one capital letter"));
            if (!password.Any(char.IsDigit)) ValidationErrors.Add(new ValidationError("You password needs to have at least one number"));

            return !ValidationErrors.Any();
        }

        public void Set(string password)
        {
            Characters = password;
        }

        public override string ToString()
            => $"[Characters: {Characters}]";

        public override int GetHashCode()
            => StringComparer.OrdinalIgnoreCase.GetHashCode(Characters);

        public bool Equals([AllowNull] Password other)
        {
            if (other == null)
                return false;

            return string.Equals(this.Characters, other.Characters) ? true : false;
        }

        public static bool operator ==(Password password1, Password password2)
            => ReferenceEquals(password1, null) ? ReferenceEquals(password2, null) : password1.Equals(password2);

        public static bool operator !=(Password password1, Password password2)
            => !(password1 == password2);
    }
}

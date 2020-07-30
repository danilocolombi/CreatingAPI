using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CreatingAPI.Domain.Core.ValueObjects
{
    public class Password : IEquatable<Password>
    {
        public string Characters { get; private set; }

        private const int MIN_LENGTH = 5;
        private const int MAX_LENGTH = 50;

        private Password(string characters)
        {
            Characters = characters;
        }

        public static implicit operator Password(string characters)
            => Parse(characters);

        public static Password Parse(string characters)
        {
            if (TryParse(characters, out var password))
                return password;

            throw new Exception("invalid password");
        }

        public static bool TryParse(string characters, out Password password)
        {
            password = new Password(characters);

            if (characters.Length < MIN_LENGTH ||
                characters.Length > MAX_LENGTH ||
                !characters.Any(char.IsUpper) ||
                !characters.Any(char.IsDigit))
                return false;

            return true;
        }

        public override string ToString()
            => $"[Characters: {Characters}]";

        public override int GetHashCode()
            => StringComparer.OrdinalIgnoreCase.GetHashCode(Characters);

        public override bool Equals(object obj)
        {
            if (obj is Password otherPassword)
                return Equals(otherPassword);

            return false;
        }

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

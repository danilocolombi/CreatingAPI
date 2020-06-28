using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace CreatingAPI.Domain.Core.ValueObjects
{
    public class Email : IEquatable<Email>
    {
        public string Address { get; private set; }

        private const int addressMaxLength = 150;
        private const int addressMinLength = 8;
        public ICollection<ValidationError> ValidationErrors = new List<ValidationError>();

        public bool IsValid(string email)
        {
            if (!CheckEmailFormat(email)) ValidationErrors.Add(new ValidationError("E-mail address format is invalid"));
            if (email.Length < addressMinLength)
                ValidationErrors.Add(new ValidationError($"E-mail address must have more than {addressMinLength} characters"));
            if (email.Length > addressMaxLength)
                ValidationErrors.Add(new ValidationError($"E-mail address must have less than {addressMaxLength} characters"));

            return !ValidationErrors.Any();
        }

        private static bool CheckEmailFormat(string email)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }

        public void Set(string address)
        {
            Address = address;
        }

        public override string ToString()
        => $"[Address: {Address}]";

        public override int GetHashCode()
        => StringComparer.OrdinalIgnoreCase.GetHashCode(Address);

        public bool Equals([AllowNull] Email other)
        {
            if (other == null)
                return false;

            return string.Equals(this.Address, other.Address) ? true : false;
        }

        public static bool operator ==(Email email1, Email email2)
            => ReferenceEquals(email1, null) ? ReferenceEquals(email2, null) : email1.Equals(email2);

        public static bool operator !=(Email email1, Email email2)
            => !(email1 == email2);
    }
}

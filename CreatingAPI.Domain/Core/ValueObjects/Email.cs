using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace CreatingAPI.Domain.Core.ValueObjects
{
    public class Email : IEquatable<Email>
    {
        public string Address { get; set; }

        private const int addressMaxLength = 150;
        private const int addressMinLength = 8;

        private Email(string address)
        {
            Address = address;
        }

        public static implicit operator Email(string address)
            => Parse(address);

        public static Email Parse(string address)
        {
            if (TryParse(address, out var email))
                return email;

            throw new Exception("Invalid email address");
        }

        public static bool TryParse(string address, out Email email)
        {
            email = new Email(address);

            if (!CheckEmailFormat(address) ||
                address.Length < addressMinLength ||
                address.Length > addressMaxLength)
                return false;

            return true;
        }

        private static bool CheckEmailFormat(string email)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
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

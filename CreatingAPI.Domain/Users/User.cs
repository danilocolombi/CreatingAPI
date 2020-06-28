using CreatingAPI.Domain.Bookmarks;
using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Core.ValueObjects;
using CreatingAPI.Domain.Games;
using CreatingAPI.Domain.Unscrumbles;
using System.Collections.Generic;

namespace CreatingAPI.Domain.Users
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public Email Email { get; private set; } = new Email();
        public Password Password { get; private set; } = new Password();
        public virtual ICollection<Unscrumble> Unscrumbles { get; private set; } = new List<Unscrumble>();
        public virtual ICollection<Bookmark> Bookmarks { get; private set; } = new List<Bookmark>();
        public virtual ICollection<Game> Games { get; set; } = new List<Game>();

        public User() { }

        public User(string name, string email, string password)
        {
            SetName(name);
            SetEmail(email);
            SetPassword(password);
        }

        public bool SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ValidationErrors.Add(new ValidationError("The name can't be empty"));
                return false;
            }
            if (name.Length < 3)
            {
                ValidationErrors.Add(new ValidationError("The name can't have less than 3 characters"));
                return false;
            }
            if (name.Length > 150)
            {
                ValidationErrors.Add(new ValidationError("The name can't have more than 150 characters"));
                return false;
            }

            Name = name;
            return true;
        }
        public void SetEmail(string email)
        {
            if (Email.IsValid(email)) Email.Set(email);
            else ValidationErrors.AddRange(Email.ValidationErrors);
        }

        public void SetPassword(string password)
        {
            if (Password.IsValid(password)) Password.Set(password);
            else ValidationErrors.AddRange(Password.ValidationErrors);
        }

        public override string ToString()
            => $"[Id: {Id}; Name: {Name}; Email: {Email.Address}]";

        public override int GetHashCode()
            => (Name, Email, Password.Characters).GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is User otherUser)
            {
                return this.Email == otherUser.Email &&
                this.Name == otherUser.Name &&
                this.Password == otherUser.Password;
            }

            return false;
        }

        public static bool operator ==(User user1, User user2)
            => ReferenceEquals(user1, null) ? ReferenceEquals(user2, null) : user1.Equals(user2);

        public static bool operator !=(User user1, User user2)
            => !(user1 == user2);
    }
}

using CreatingAPI.Domain.Bookmarks;
using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Core.ValueObjects;
using CreatingAPI.Domain.Games;
using CreatingAPI.Domain.Pickers;
using CreatingAPI.Domain.TicTacToes;
using CreatingAPI.Domain.Unscrambles;
using System.Collections.Generic;

namespace CreatingAPI.Domain.Users
{
    public class User : Entity
    {
        public string Name { get; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public virtual ICollection<Unscramble> Unscrambles { get;}
        public virtual ICollection<Bookmark> Bookmarks { get;}
        public virtual ICollection<Game> Games { get; }
        public virtual ICollection<TicTacToe> TicTacToes { get; }
        public virtual ICollection<Picker> Pickers { get; }

        public User() { }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public void SetPassword(string password)
        {
            Password = password;
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

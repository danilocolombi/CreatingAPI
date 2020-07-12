using CreatingAPI.Domain.Activities;
using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Linq;

namespace CreatingAPI.Domain.TicTacToes
{
    public class TicTacToe : Activity
    {
        public ICollection<TicTacToeSquare> Squares { get; set; }

        private const int numberOfSquares = 9;

        public TicTacToe() { }
        public TicTacToe(string title, int userId, bool isPublic) : base(title, userId, isPublic)
        {
        }

        public bool CreateSquares(IEnumerable<TicTacToeSquare> squares)
        {
            if (squares.Count() != numberOfSquares)
                ValidationErrors.Add(new ValidationError($"A TicTacToe needs {numberOfSquares} squares"));

            Squares = new List<TicTacToeSquare>(numberOfSquares);

            foreach (var square in squares)
            {
                if (!square.IsValid())
                {
                    ValidationErrors.Add(new ValidationError($"Square {square.Position} is invalid: {square.ValidationErrors.FirstOrDefault()}"));
                    Squares.Clear();
                    return false;
                }

                Squares.Add(square);
            }

            return true;
        }

        public override string ToString()
            => base.ToString();

        public override int GetHashCode()
            => base.GetHashCode();

        public override bool Equals(object obj)
        {
            var otherTicTacToe = obj as TicTacToe;

            if (otherTicTacToe == null) return false;

            if (string.Equals(this.Title, otherTicTacToe.Title) &&
                this.CreatedAt == otherTicTacToe.CreatedAt &&
                this.IsPublic == otherTicTacToe.IsPublic &&
                this.UserId == otherTicTacToe.UserId)
                return true;

            return false;
        }

        public static bool operator ==(TicTacToe ticTacToe1, TicTacToe ticTacToe2)
            => ReferenceEquals(ticTacToe1, null) ? ReferenceEquals(ticTacToe2, null) : ticTacToe1.Equals(ticTacToe2);

        public static bool operator !=(TicTacToe ticTacToe1, TicTacToe ticTacToe2)
          => !(ticTacToe1 == ticTacToe2);
    }
}

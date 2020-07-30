using CreatingAPI.Domain.Activities;
using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Linq;

namespace CreatingAPI.Domain.TicTacToes
{
    public class TicTacToe : Activity
    {
        public ICollection<TicTacToeSquare> Squares { get; set; }

        private const int NUMBER_OF_SQUARES = 9;

        public TicTacToe() { }
        public TicTacToe(string title, int userId, bool isPublic) : base(title, userId, isPublic)
        {
        }

        public bool AddSquares(IEnumerable<TicTacToeSquare> squares)
        {
            if (squares.Count() != NUMBER_OF_SQUARES)
            {
                ValidationErrors.Add(new ValidationError($"A TicTacToe needs {NUMBER_OF_SQUARES} squares"));
                return false;
            }

            Squares = new List<TicTacToeSquare>(NUMBER_OF_SQUARES);

            foreach (var square in squares)
            {
                if (!square.IsValid())
                {
                    ValidationErrors.Add(new ValidationError($"Square {square.Position} is invalid: {square.ValidationErrors.FirstOrDefault()}"));
                    return false;
                }

                var squaresInThisPosition = squares.Where(p => p.Position == square.Position).Count();

                if (squaresInThisPosition != 1)
                {
                    ValidationErrors.Add(new ValidationError($"You can't have multiple squares in the same position"));
                    return false;
                }

                if (Id > 0)
                    square.TicTacToeId = Id;

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

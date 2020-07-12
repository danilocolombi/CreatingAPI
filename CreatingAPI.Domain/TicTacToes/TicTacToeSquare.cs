using CreatingAPI.Domain.Core;

namespace CreatingAPI.Domain.TicTacToes
{
    public class TicTacToeSquare : Entity
    {
        public string Description { get; private set; }
        public int Position { get; private set; }
        public int TicTacToeId { get; set; }
        public virtual TicTacToe TicTacToe { get; set; }


        public TicTacToeSquare(string description, int position)
        {
            Description = description;
            Position = position;
        }

        public override string ToString()
            => $"[Id: {Id}; Description: {Description}; Position: {Position}";

        public override int GetHashCode()
            => (Description, Position).GetHashCode();

        public override bool Equals(object obj)
        {
            var otherTicTacToeSquare = obj as TicTacToeSquare;

            if (otherTicTacToeSquare == null)
                return false;

            if (string.Compare(this.Description, otherTicTacToeSquare.Description) == 0 &&
                this.Position == Position)
                return true;

            return false;
        }

        public static bool operator ==(TicTacToeSquare ticTacToeSquare1, TicTacToeSquare ticTacToeSquare2)
            => ReferenceEquals(ticTacToeSquare1, null) ? ReferenceEquals(ticTacToeSquare2, null) : ticTacToeSquare1.Equals(ticTacToeSquare2);

        public static bool operator !=(TicTacToeSquare ticTacToeSquare1, TicTacToeSquare ticTacToeSquare2)
            => !(ticTacToeSquare1 == ticTacToeSquare2);
    }
}

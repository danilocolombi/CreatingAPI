using FluentAssertions;
using System.Linq;
using Xunit;

namespace CreatingAPI.Domain.Tests.TicTacToes
{
    public class TicTacToeTests
    {
        [Fact(DisplayName = "Create Squares, should return true")]
        [Trait("Category", "Create Squares")]
        public void CreateSquares_ShouldReturnTrue()
        {
            var ticTacToe = TicTacToeTestHelper.GetFakeTicTacToe();
            var squares = TicTacToeTestHelper.GetFakeTicTacToeSquares();

            var result = ticTacToe.CreateSquares(squares);

            result.Should().BeTrue();
            ticTacToe.Squares.Should().BeEquivalentTo(squares);
        }

        [Fact(DisplayName = "Create squares with an invalid number of squares, should return false")]
        [Trait("Category", "Create Squares")]
        public void CreateSquares_InvalidNumberOfSquares_ShouldReturnFalse()
        {
            var ticTacToe = TicTacToeTestHelper.GetFakeTicTacToe();
            var squares = TicTacToeTestHelper.GetFakeTicTacToeSquares().ToList();
            squares.RemoveAt(0);

            var result = ticTacToe.CreateSquares(squares);

            result.Should().BeFalse();
            ticTacToe.ValidationErrors.FirstOrDefault().Message.Should().Be($"A TicTacToe needs 9 squares");
        }

        [Fact(DisplayName = "Create squares with invalid squares, should return false")]
        [Trait("Category", "Create Squares")]
        public void CreateSquares_InvalidSquares_ShouldReturnFalse()
        {
            var ticTacToe = TicTacToeTestHelper.GetFakeTicTacToe();
            var squares = TicTacToeTestHelper.GetFakeInvalidTicTacToeSquares();

            var result = ticTacToe.CreateSquares(squares);

            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Create squares with squares in the same position, should return false")]
        [Trait("Category", "Create Squares")]
        public void CreateSquares_SquaresInTheSamePosition_ShouldReturnFalse()
        {
            var ticTacToe = TicTacToeTestHelper.GetFakeTicTacToe();
            var squares = TicTacToeTestHelper.GetFakeTicTacToeSquares();
            squares.FirstOrDefault().SetPosition(2);

            var result = ticTacToe.CreateSquares(squares);

            result.Should().BeFalse();
            ticTacToe.ValidationErrors.FirstOrDefault().Message.Should().Be("You can't have multiple squares in the same position");
        }
    }
}

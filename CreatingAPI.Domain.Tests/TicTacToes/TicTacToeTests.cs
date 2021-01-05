using FluentAssertions;
using System.Linq;
using Xunit;

namespace CreatingAPI.Domain.Tests.TicTacToes
{
    [Collection(nameof(TicTacToeTestsFixtureCollection))]
    public class TicTacToeTests
    {
        private readonly TicTacToeTestsFixture _ticTacToeTestsFixture;

        public TicTacToeTests(TicTacToeTestsFixture ticTacToeTestsFixture)
        {
            _ticTacToeTestsFixture = ticTacToeTestsFixture;
        }

        [Fact(DisplayName = "Add squares, should return true")]
        [Trait("Category", "Add Squares")]
        public void AddSquares_ShouldReturnTrue()
        {
            var ticTacToe = _ticTacToeTestsFixture.GetFakeTicTacToe();
            var squares = _ticTacToeTestsFixture.GetFakeTicTacToeSquares();

            var result = ticTacToe.AddSquares(squares);

            result.Should().BeTrue();
            ticTacToe.Squares.Should().BeEquivalentTo(squares);
        }

        [Fact(DisplayName = "Add squares with an invalid number of squares, should return false")]
        [Trait("Category", "Add Squares")]
        public void AddSquares_InvalidNumberOfSquares_ShouldReturnFalse()
        {
            var ticTacToe = _ticTacToeTestsFixture.GetFakeTicTacToe();
            var squares = _ticTacToeTestsFixture.GetFakeTicTacToeSquares().ToList();
            squares.RemoveAt(0);

            var result = ticTacToe.AddSquares(squares);

            result.Should().BeFalse();
            ticTacToe.ValidationErrors.FirstOrDefault().Message.Should().Be($"A TicTacToe needs 9 squares");
        }

        [Fact(DisplayName = "Add squares with invalid squares, should return false")]
        [Trait("Category", "Add Squares")]
        public void AddSquares_InvalidSquares_ShouldReturnFalse()
        {
            var ticTacToe = _ticTacToeTestsFixture.GetFakeTicTacToe();
            var squares = _ticTacToeTestsFixture.GetFakeInvalidTicTacToeSquares();

            var result = ticTacToe.AddSquares(squares);

            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Add squares with squares in the same position, should return false")]
        [Trait("Category", "Add Squares")]
        public void AddSquares_SquaresInTheSamePosition_ShouldReturnFalse()
        {
            var ticTacToe = _ticTacToeTestsFixture.GetFakeTicTacToe();
            var squares = _ticTacToeTestsFixture.GetFakeTicTacToeSquares();
            squares.FirstOrDefault().SetPosition(2);

            var result = ticTacToe.AddSquares(squares);

            result.Should().BeFalse();
            ticTacToe.ValidationErrors.FirstOrDefault().Message.Should().Be("You can't have multiple squares in the same position");
        }
    }
}

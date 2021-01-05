using CreatingAPI.Domain.TicTacToes;
using CreatingAPI.Domain.TicTacToes.Interfaces;
using CreatingAPI.Domain.TicTacToes.Services;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CreatingAPI.Domain.Tests.TicTacToes
{
    [Collection(nameof(TicTacToeTestsFixtureCollection))]
    public class TicTacToeServiceTests
    {
        private readonly Mock<ITicTacToeRepository> _repositoryMock;
        private readonly TicTacToeTestsFixture _ticTacToeTestsFixture;
        private readonly TicTacToeService _ticTacToeService;

        public TicTacToeServiceTests(TicTacToeTestsFixture ticTacToeTestsFixture)
        {
            _ticTacToeTestsFixture = ticTacToeTestsFixture;
            _repositoryMock = _ticTacToeTestsFixture.GetTicTacToeRepositoryMock();
            _ticTacToeService = new TicTacToeService(_repositoryMock.Object);
        }

        [Fact(DisplayName = "Create ticTacToe with success, should return ResultResponse with success")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_ShouldReturnResultResponseWithSuccess()
        {
            var ticTacToe = _ticTacToeTestsFixture.GetFakeTicTacToe();
            var squares = _ticTacToeTestsFixture.GetFakeTicTacToeSquares();

            var result = await _ticTacToeService.CreateAsync(ticTacToe, squares);

            result.Success.Should().BeTrue();
            ticTacToe.IsValid().Should().BeTrue();
            _repositoryMock.Verify(rm => rm.CreateAsync(ticTacToe), Times.Once);
        }

        [Fact(DisplayName = "Create ticTacToe with an invalid square, should return ResultResponse with error")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_InvalidSquare_ShouldReturnResultResponseWithError()
        {
            var ticTacToe = _ticTacToeTestsFixture.GetFakeTicTacToe();
            var squares = _ticTacToeTestsFixture.GetFakeInvalidTicTacToeSquares();

            var result = await _ticTacToeService.CreateAsync(ticTacToe, squares);

            result.Success.Should().BeFalse();
            _repositoryMock.Verify(rm => rm.CreateAsync(It.IsAny<TicTacToe>()), Times.Never);
        }

        [Fact(DisplayName = "Update ticTacToe with success, should return ResultResponse with success")]
        [Trait("Category", "Update")]
        public async Task UpdateAsync_ShouldReturnResultResponseWithSuccess()
        {
            var ticTacToe = _ticTacToeTestsFixture.GetFakeTicTacToe();
            var squares = _ticTacToeTestsFixture.GetFakeTicTacToeSquares();
            var id = _ticTacToeTestsFixture.GetRandomInt();

            var result = await _ticTacToeService.UpdateAsync(id, ticTacToe, squares);

            result.Success.Should().BeTrue();
            ticTacToe.IsValid().Should().BeTrue();
            _repositoryMock.Verify(rm => rm.UpdateAsync(ticTacToe), Times.Once);
        }

        [Theory(DisplayName = "Update ticTacToe with an invalid id, should return ResultResponse with success")]
        [Trait("Category", "Update")]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task UpdateAsync_InvalidId_ShouldReturnResultResponseWithError(int invalidId)
        {
            var ticTacToe = _ticTacToeTestsFixture.GetFakeTicTacToe();
            var squares = _ticTacToeTestsFixture.GetFakeTicTacToeSquares();

            var result = await _ticTacToeService.UpdateAsync(invalidId, ticTacToe, squares);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(rm => rm.UpdateAsync(It.IsAny<TicTacToe>()), Times.Never);
        }

        [Fact(DisplayName = "Delete ticTacToe with success, should return ResultResponse with success")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_ShouldReturnResultResponseWithSuccess()
        {
            var id = _ticTacToeTestsFixture.GetRandomInt();
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await _ticTacToeService.DeleteAsync(id);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<TicTacToe>()), Times.Once);
        }

        [Theory(DisplayName = "Delete ticTacToe with invalid id, should return ResultResponse with error")]
        [Trait("Category", "Delete")]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task DeleteAsync_InvalidId_ShouldReturnResultResponseWithError(int invalidId)
        {
            var result = await _ticTacToeService.DeleteAsync(invalidId);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<TicTacToe>()), Times.Never);
        }

        [Fact(DisplayName = "Delete ticTacToe with inexistent id, should return ResultResponse with error")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_InexistentId_ShouldReturnResultResponseWithError()
        {
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await _ticTacToeService.DeleteAsync(_ticTacToeTestsFixture.GetInexistentTicTacToeId());

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity wasn't found");
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<TicTacToe>()), Times.Never);
        }
    }
}

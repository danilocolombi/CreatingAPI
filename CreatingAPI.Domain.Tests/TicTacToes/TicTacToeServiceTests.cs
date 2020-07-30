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
    public class TicTacToeServiceTests
    {
        private readonly Mock<ITicTacToeRepository> _repositoryMock;

        private const int ID_INEXISTENT_TIC_TAC_TOE = 1;

        public TicTacToeServiceTests()
        {
            _repositoryMock = new Mock<ITicTacToeRepository>(MockBehavior.Loose);
            _repositoryMock.Setup(rm => rm.CreateAsync(It.IsAny<TicTacToe>())).ReturnsAsync(TicTacToeTestHelper.GetRandomInt());
            _repositoryMock.Setup(rm => rm.UpdateAsync(It.IsAny<TicTacToe>())).ReturnsAsync(true);
            _repositoryMock.Setup(rm => rm.DeleteAsync(It.IsAny<TicTacToe>())).ReturnsAsync(true);
            _repositoryMock.Setup(rm => rm.GetAsync(It.Is<int>(i => i != ID_INEXISTENT_TIC_TAC_TOE))).ReturnsAsync(TicTacToeTestHelper.GetFakeTicTacToe());
            _repositoryMock.Setup(rm => rm.GetAsync(It.Is<int>(i => i == ID_INEXISTENT_TIC_TAC_TOE))).ReturnsAsync((TicTacToe)null);
        }

        [Fact(DisplayName = "Create ticTacToe with success, should return ResultResponse with success")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_ShouldReturnResultResponseWithSuccess()
        {
            var ticTacToe = TicTacToeTestHelper.GetFakeTicTacToe();
            var squares = TicTacToeTestHelper.GetFakeTicTacToeSquares();
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.CreateAsync(ticTacToe, squares);

            result.Success.Should().BeTrue();
            ticTacToe.IsValid().Should().BeTrue();
            _repositoryMock.Verify(rm => rm.CreateAsync(ticTacToe), Times.Once);
        }

        [Fact(DisplayName = "Create an invalid ticTacToe, should return ResultResponse with error")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_InvalidTicTacToe_ShouldReturnResultResponseWithError()
        {
            var invalidTicTacToe = TicTacToeTestHelper.GetFakeInvalidTicTacToe();
            var squares = TicTacToeTestHelper.GetFakeTicTacToeSquares();
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.CreateAsync(invalidTicTacToe, squares);

            result.Success.Should().BeFalse();
            invalidTicTacToe.IsValid().Should().BeFalse();
            _repositoryMock.Verify(rm => rm.CreateAsync(It.IsAny<TicTacToe>()), Times.Never);
        }

        [Fact(DisplayName = "Create ticTacToe with an invalid square, should return ResultResponse with error")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_InvalidSquare_ShouldReturnResultResponseWithError()
        {
            var ticTacToe = TicTacToeTestHelper.GetFakeTicTacToe();
            var squares = TicTacToeTestHelper.GetFakeInvalidTicTacToeSquares();
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.CreateAsync(ticTacToe, squares);

            result.Success.Should().BeFalse();
            _repositoryMock.Verify(rm => rm.CreateAsync(It.IsAny<TicTacToe>()), Times.Never);
        }

        [Fact(DisplayName = "Update ticTacToe with success, should return ResultResponse with success")]
        [Trait("Category", "Update")]
        public async Task UpdateAsync_ShouldReturnResultResponseWithSuccess()
        {
            var ticTacToe = TicTacToeTestHelper.GetFakeTicTacToe();
            var squares = TicTacToeTestHelper.GetFakeTicTacToeSquares();
            var id = TicTacToeTestHelper.GetRandomInt();
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.UpdateAsync(id, ticTacToe, squares);

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
            var ticTacToe = TicTacToeTestHelper.GetFakeTicTacToe();
            var squares = TicTacToeTestHelper.GetFakeTicTacToeSquares();
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.UpdateAsync(invalidId, ticTacToe, squares);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(rm => rm.UpdateAsync(It.IsAny<TicTacToe>()), Times.Never);
        }

        [Fact(DisplayName = "Update an invalid ticTacToe, should return ResultResponse with error")]
        [Trait("Category", "Update")]
        public async Task UpdateAsync_InvalidTicTacToe_ShouldReturnResultResponseWithError()
        {
            var invalidTicTacToe = TicTacToeTestHelper.GetFakeInvalidTicTacToe();
            var squares = TicTacToeTestHelper.GetFakeTicTacToeSquares();
            var id = TicTacToeTestHelper.GetRandomInt();
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.UpdateAsync(id, invalidTicTacToe, squares);

            result.Success.Should().BeFalse();
            result.ValidationErrors.Should().NotBeEmpty();
            _repositoryMock.Verify(rm => rm.UpdateAsync(It.IsAny<TicTacToe>()), Times.Never);
        }

        [Fact(DisplayName = "Delete ticTacToe with success, should return ResultResponse with success")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_ShouldReturnResultResponseWithSuccess()
        {
            var id = TicTacToeTestHelper.GetRandomInt();
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.DeleteAsync(id);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<TicTacToe>()), Times.Once);
        }

        [Theory(DisplayName = "Delete ticTacToe with invalid id, should return ResultResponse with error")]
        [Trait("Category", "Delete")]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task DeleteAsync_InvalidId_ShouldReturnResultResponseWithError(int invalidId)
        {
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.DeleteAsync(invalidId);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<TicTacToe>()), Times.Never);
        }

        [Fact(DisplayName = "Delete ticTacToe with inexistent id, should return ResultResponse with error")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_InexistentId_ShouldReturnResultResponseWithError()
        {
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.DeleteAsync(ID_INEXISTENT_TIC_TAC_TOE);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity wasn't found");
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<TicTacToe>()), Times.Never);
        }
    }
}

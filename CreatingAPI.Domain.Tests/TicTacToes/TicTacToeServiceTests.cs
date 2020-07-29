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
            _repositoryMock.Setup(rm => rm.CreateTicTacToe(It.IsAny<TicTacToe>())).ReturnsAsync(TicTacToeTestHelper.GetRandomInt());
            _repositoryMock.Setup(rm => rm.UpdateTicTacToe(It.IsAny<TicTacToe>())).ReturnsAsync(true);
            _repositoryMock.Setup(rm => rm.DeleteTicTacToe(It.IsAny<TicTacToe>())).ReturnsAsync(true);
            _repositoryMock.Setup(rm => rm.GetTicTacToe(It.Is<int>(i => i != ID_INEXISTENT_TIC_TAC_TOE))).ReturnsAsync(TicTacToeTestHelper.GetFakeTicTacToe());
            _repositoryMock.Setup(rm => rm.GetTicTacToe(It.Is<int>(i => i == ID_INEXISTENT_TIC_TAC_TOE))).ReturnsAsync((TicTacToe)null);
        }

        [Fact(DisplayName = "Create ticTacToe with success, should return ResultResponse with success")]
        [Trait("Category", "Create TicTacToe")]
        public async Task CreateTicTacToe_ShouldReturnResultResponseWithSuccess()
        {
            var ticTacToe = TicTacToeTestHelper.GetFakeTicTacToe();
            var squares = TicTacToeTestHelper.GetFakeTicTacToeSquares();
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.CreateTicTacToe(ticTacToe, squares);

            result.Success.Should().BeTrue();
            ticTacToe.IsValid().Should().BeTrue();
            _repositoryMock.Verify(rm => rm.CreateTicTacToe(ticTacToe), Times.Once);
        }

        [Fact(DisplayName = "Create an invalid ticTacToe, should return ResultResponse with error")]
        [Trait("Category", "Create TicTacToe")]
        public async Task CreateTicTacToe_InvalidTicTacToe_ShouldReturnResultResponseWithError()
        {
            var invalidTicTacToe = TicTacToeTestHelper.GetFakeInvalidTicTacToe();
            var squares = TicTacToeTestHelper.GetFakeTicTacToeSquares();
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.CreateTicTacToe(invalidTicTacToe, squares);

            result.Success.Should().BeFalse();
            invalidTicTacToe.IsValid().Should().BeFalse();
            _repositoryMock.Verify(rm => rm.CreateTicTacToe(It.IsAny<TicTacToe>()), Times.Never);
        }

        [Fact(DisplayName = "Create ticTacToe with an invalid square, should return ResultResponse with error")]
        [Trait("Category", "Create TicTacToe")]
        public async Task CreateTicTacToe_InvalidSquare_ShouldReturnResultResponseWithError()
        {
            var ticTacToe = TicTacToeTestHelper.GetFakeTicTacToe();
            var squares = TicTacToeTestHelper.GetFakeInvalidTicTacToeSquares();
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.CreateTicTacToe(ticTacToe, squares);

            result.Success.Should().BeFalse();
            _repositoryMock.Verify(rm => rm.CreateTicTacToe(It.IsAny<TicTacToe>()), Times.Never);
        }

        [Fact(DisplayName = "Update ticTacToe with success, should return ResultResponse with success")]
        [Trait("Category", "Update TicTacToe")]
        public async Task UpdateTicTacToe_ShouldReturnResultResponseWithSuccess()
        {
            var ticTacToe = TicTacToeTestHelper.GetFakeTicTacToe();
            var squares = TicTacToeTestHelper.GetFakeTicTacToeSquares();
            var id = TicTacToeTestHelper.GetRandomInt();
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.UpdateTicTacToe(id, ticTacToe, squares);

            result.Success.Should().BeTrue();
            ticTacToe.IsValid().Should().BeTrue();
            _repositoryMock.Verify(rm => rm.UpdateTicTacToe(ticTacToe), Times.Once);
        }

        [Theory(DisplayName = "Update ticTacToe with an invalid id, should return ResultResponse with success")]
        [Trait("Category", "Update TicTacToe")]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task UpdateTicTacToe_InvalidId_ShouldReturnResultResponseWithError(int invalidId)
        {
            var ticTacToe = TicTacToeTestHelper.GetFakeTicTacToe();
            var squares = TicTacToeTestHelper.GetFakeTicTacToeSquares();
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.UpdateTicTacToe(invalidId, ticTacToe, squares);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(rm => rm.UpdateTicTacToe(It.IsAny<TicTacToe>()), Times.Never);
        }

        [Fact(DisplayName = "Update an invalid ticTacToe, should return ResultResponse with error")]
        [Trait("Category", "Update TicTacToe")]
        public async Task UpdateTicTacToe_InvalidTicTacToe_ShouldReturnResultResponseWithError()
        {
            var invalidTicTacToe = TicTacToeTestHelper.GetFakeInvalidTicTacToe();
            var squares = TicTacToeTestHelper.GetFakeTicTacToeSquares();
            var id = TicTacToeTestHelper.GetRandomInt();
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.UpdateTicTacToe(id, invalidTicTacToe, squares);

            result.Success.Should().BeFalse();
            result.ValidationErrors.Should().NotBeEmpty();
            _repositoryMock.Verify(rm => rm.UpdateTicTacToe(It.IsAny<TicTacToe>()), Times.Never);
        }

        [Fact(DisplayName = "Delete ticTacToe with success, should return ResultResponse with success")]
        [Trait("Category", "Delete TicTacToe")]
        public async Task DeleteTicTacToe_ShouldReturnResultResponseWithSuccess()
        {
            var id = TicTacToeTestHelper.GetRandomInt();
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.DeleteTicTacToe(id);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.DeleteTicTacToe(It.IsAny<TicTacToe>()), Times.Once);
        }

        [Theory(DisplayName = "Delete ticTacToe with invalid id, should return ResultResponse with error")]
        [Trait("Category", "Delete TicTacToe")]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task DeleteTicTacToe_InvalidId_ShouldReturnResultResponseWithError(int invalidId)
        {
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.DeleteTicTacToe(invalidId);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(rm => rm.DeleteTicTacToe(It.IsAny<TicTacToe>()), Times.Never);
        }

        [Fact(DisplayName = "Delete ticTacToe with inexistent id, should return ResultResponse with error")]
        [Trait("Category", "Delete TicTacToe")]
        public async Task DeleteTicTacToe_InexistentId_ShouldReturnResultResponseWithError()
        {
            var ticTacToeService = new TicTacToeService(_repositoryMock.Object);

            var result = await ticTacToeService.DeleteTicTacToe(ID_INEXISTENT_TIC_TAC_TOE);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity wasn't found");
            _repositoryMock.Verify(rm => rm.DeleteTicTacToe(It.IsAny<TicTacToe>()), Times.Never);
        }
    }
}

using CreatingAPI.Domain.Unscrambles;
using CreatingAPI.Domain.Unscrambles.Interfaces;
using CreatingAPI.Domain.Unscrambles.Services;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CreatingAPI.Domain.Tests.Unscrambles
{
    public class UnscrambleServiceTests
    {
        private readonly Mock<IUnscrambleRepository> _repositoryMock;

        private const int ID_INEXISTENT_UNSCRAMBLE = 1;
        public UnscrambleServiceTests()
        {
            _repositoryMock = new Mock<IUnscrambleRepository>(MockBehavior.Loose);
            _repositoryMock.Setup(mr => mr.CreateUnscramble(It.IsAny<Unscramble>())).ReturnsAsync(UnscrambleTestHelper.GetRandomInt());
            _repositoryMock.Setup(mr => mr.UpdateUnscramble(It.IsAny<Unscramble>())).ReturnsAsync(true);
            _repositoryMock.Setup(mr => mr.DeleteUnscramble(It.IsAny<Unscramble>())).ReturnsAsync(true);
            _repositoryMock.Setup(mr => mr.GetUnscramble(It.Is<int>(i => i != ID_INEXISTENT_UNSCRAMBLE))).ReturnsAsync(UnscrambleTestHelper.GetFakeUnscramble());
            _repositoryMock.Setup(mr => mr.GetUnscramble(It.Is<int>(i => i == ID_INEXISTENT_UNSCRAMBLE))).ReturnsAsync((Unscramble)null);
        }

        [Fact(DisplayName = "Create unscramble with success, should return ResultResponse with success")]
        [Trait("Category", "Create Unscramble")]
        public async Task CreateUnscramble_ShouldReturnResultResponseWithSuccess()
        {
            var unscramble = UnscrambleTestHelper.GetFakeUnscramble();
            var exercises = UnscrambleTestHelper.GetFakeExercises();
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrambleService.CreateUnscramble(unscramble, exercises);

            result.Success.Should().BeTrue();
            unscramble.IsValid().Should().BeTrue();
            _repositoryMock.Verify(mr => mr.CreateUnscramble(unscramble), Times.Once);
        }

        [Fact(DisplayName = "Create an invalid unscramble, should return ResultResponse with error")]
        [Trait("Category", "Create Unscramble")]
        public async Task CreateUnscramble_InvalidUnscramble_ShouldReturnResultResponseWithError()
        {
            var invalidUnscramble = UnscrambleTestHelper.GetFakeInvalidUnscramble();
            var exercises = UnscrambleTestHelper.GetFakeExercises();
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrambleService.CreateUnscramble(invalidUnscramble, exercises);

            result.Success.Should().BeFalse();
            invalidUnscramble.IsValid().Should().BeFalse();
            _repositoryMock.Verify(mr => mr.CreateUnscramble(It.IsAny<Unscramble>()), Times.Never);
        }

        [Fact(DisplayName = "Update unscramble with success, should return ResultResponse with success")]
        [Trait("Category", "Update Unscramble")]
        public async Task UpdateUnscramble_ShouldReturnResultResponseWithSuccess()
        {
            var unscramble = UnscrambleTestHelper.GetFakeUnscramble();
            var id = UnscrambleTestHelper.GetRandomInt();
            var exercises = UnscrambleTestHelper.GetFakeExercises();
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrambleService.UpdateUnscramble(id, unscramble, exercises);

            result.Success.Should().BeTrue();
            unscramble.IsValid().Should().BeTrue();
            _repositoryMock.Verify(mr => mr.UpdateUnscramble(unscramble), Times.Once);
        }

        [Theory(DisplayName = "Update unscramble with invalid id, should return ResultResponse with error")]
        [Trait("Category", "Update Unscramble")]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task UpdateUnscramble_InvalidId_ShouldReturnResultResponseWithError(int invalidId)
        {
            var unscramble = UnscrambleTestHelper.GetFakeUnscramble();
            var exercises = UnscrambleTestHelper.GetFakeExercises();
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrambleService.UpdateUnscramble(invalidId, unscramble, exercises);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(mr => mr.UpdateUnscramble(It.IsAny<Unscramble>()), Times.Never);
        }

        [Fact(DisplayName = "Update an invalid unscramble, should return ResultResponse with error")]
        [Trait("Category", "Update Unscramble")]
        public async Task UpdateUnscramble_InvalidUnscramble_ShouldReturnResultResponseWithError()
        {
            var invalidUnscramble = UnscrambleTestHelper.GetFakeInvalidUnscramble();
            var id = UnscrambleTestHelper.GetRandomInt();
            var exercises = UnscrambleTestHelper.GetFakeExercises();
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrambleService.UpdateUnscramble(id, invalidUnscramble, exercises);

            result.Success.Should().BeFalse();
            result.ValidationErrors.Should().NotBeEmpty();
            _repositoryMock.Verify(mr => mr.UpdateUnscramble(It.IsAny<Unscramble>()), Times.Never);
        }

        [Fact(DisplayName = "Delete unscramble with success, should return ResultResponse with success")]
        [Trait("Category", "Delete Unscramble")]
        public async Task DeleteUnscramble_ShouldReturnResultResponseWithSuccess()
        {
            var id = UnscrambleTestHelper.GetRandomInt();
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrambleService.DeleteUnscramble(id);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(mr => mr.DeleteUnscramble(It.IsAny<Unscramble>()), Times.Once);
        }

        [Theory(DisplayName = "Delete unscramble with invalid id, should return ResultResponse with error")]
        [Trait("Category", "Delete Unscramble")]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task DeleteUnscramble_InvalidId_ShouldReturnResultResponseWithError(int invalidId)
        {
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrambleService.DeleteUnscramble(invalidId);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(mr => mr.DeleteUnscramble(It.IsAny<Unscramble>()), Times.Never);
        }

        [Fact(DisplayName = "Delete unscramble with inexistent id, should return ResultResponse with error")]
        [Trait("Category", "Delete Unscramble")]
        public async Task DeleteUnscramble_InexistentId_ShouldReturnResultResponseWithError()
        {
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrambleService.DeleteUnscramble(ID_INEXISTENT_UNSCRAMBLE);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity wasn't found");
            _repositoryMock.Verify(mr => mr.DeleteUnscramble(It.IsAny<Unscramble>()), Times.Never);
        }
    }
}
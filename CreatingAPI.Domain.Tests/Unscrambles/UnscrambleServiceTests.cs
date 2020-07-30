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
            _repositoryMock.Setup(mr => mr.CreateAsync(It.IsAny<Unscramble>())).ReturnsAsync(UnscrambleTestHelper.GetRandomInt());
            _repositoryMock.Setup(mr => mr.UpdateAsync(It.IsAny<Unscramble>())).ReturnsAsync(true);
            _repositoryMock.Setup(mr => mr.DeleteAsync(It.IsAny<Unscramble>())).ReturnsAsync(true);
            _repositoryMock.Setup(mr => mr.GetAsync(It.Is<int>(i => i != ID_INEXISTENT_UNSCRAMBLE))).ReturnsAsync(UnscrambleTestHelper.GetFakeUnscramble());
            _repositoryMock.Setup(mr => mr.GetAsync(It.Is<int>(i => i == ID_INEXISTENT_UNSCRAMBLE))).ReturnsAsync((Unscramble)null);
        }

        [Fact(DisplayName = "Create unscramble with success, should return ResultResponse with success")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_ShouldReturnResultResponseWithSuccess()
        {
            var unscramble = UnscrambleTestHelper.GetFakeUnscramble();
            var exercises = UnscrambleTestHelper.GetFakeExercises();
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrambleService.CreateAsync(unscramble, exercises);

            result.Success.Should().BeTrue();
            unscramble.IsValid().Should().BeTrue();
            _repositoryMock.Verify(mr => mr.CreateAsync(unscramble), Times.Once);
        }

        [Fact(DisplayName = "Create an invalid unscramble, should return ResultResponse with error")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_InvalidUnscramble_ShouldReturnResultResponseWithError()
        {
            var invalidUnscramble = UnscrambleTestHelper.GetFakeInvalidUnscramble();
            var exercises = UnscrambleTestHelper.GetFakeExercises();
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrambleService.CreateAsync(invalidUnscramble, exercises);

            result.Success.Should().BeFalse();
            invalidUnscramble.IsValid().Should().BeFalse();
            _repositoryMock.Verify(mr => mr.CreateAsync(It.IsAny<Unscramble>()), Times.Never);
        }

        [Fact(DisplayName = "Update unscramble with success, should return ResultResponse with success")]
        [Trait("Category", "Update")]
        public async Task UpdateAsync_ShouldReturnResultResponseWithSuccess()
        {
            var unscramble = UnscrambleTestHelper.GetFakeUnscramble();
            var id = UnscrambleTestHelper.GetRandomInt();
            var exercises = UnscrambleTestHelper.GetFakeExercises();
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrambleService.UpdateAsync(id, unscramble, exercises);

            result.Success.Should().BeTrue();
            unscramble.IsValid().Should().BeTrue();
            _repositoryMock.Verify(mr => mr.UpdateAsync(unscramble), Times.Once);
        }

        [Theory(DisplayName = "Update unscramble with invalid id, should return ResultResponse with error")]
        [Trait("Category", "Update")]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task UpdateAsync_InvalidId_ShouldReturnResultResponseWithError(int invalidId)
        {
            var unscramble = UnscrambleTestHelper.GetFakeUnscramble();
            var exercises = UnscrambleTestHelper.GetFakeExercises();
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrambleService.UpdateAsync(invalidId, unscramble, exercises);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(mr => mr.UpdateAsync(It.IsAny<Unscramble>()), Times.Never);
        }

        [Fact(DisplayName = "Update an invalid unscramble, should return ResultResponse with error")]
        [Trait("Category", "Update")]
        public async Task UpdateAsync_InvalidUnscramble_ShouldReturnResultResponseWithError()
        {
            var invalidUnscramble = UnscrambleTestHelper.GetFakeInvalidUnscramble();
            var id = UnscrambleTestHelper.GetRandomInt();
            var exercises = UnscrambleTestHelper.GetFakeExercises();
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrambleService.UpdateAsync(id, invalidUnscramble, exercises);

            result.Success.Should().BeFalse();
            result.ValidationErrors.Should().NotBeEmpty();
            _repositoryMock.Verify(mr => mr.UpdateAsync(It.IsAny<Unscramble>()), Times.Never);
        }

        [Fact(DisplayName = "Delete unscramble with success, should return ResultResponse with success")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_ShouldReturnResultResponseWithSuccess()
        {
            var id = UnscrambleTestHelper.GetRandomInt();
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrambleService.DeleteAsync(id);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(mr => mr.DeleteAsync(It.IsAny<Unscramble>()), Times.Once);
        }

        [Theory(DisplayName = "Delete unscramble with invalid id, should return ResultResponse with error")]
        [Trait("Category", "Delete")]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task DeleteAsync_InvalidId_ShouldReturnResultResponseWithError(int invalidId)
        {
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrambleService.DeleteAsync(invalidId);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(mr => mr.DeleteAsync(It.IsAny<Unscramble>()), Times.Never);
        }

        [Fact(DisplayName = "Delete unscramble with inexistent id, should return ResultResponse with error")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_InexistentId_ShouldReturnResultResponseWithError()
        {
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrambleService.DeleteAsync(ID_INEXISTENT_UNSCRAMBLE);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity wasn't found");
            _repositoryMock.Verify(mr => mr.DeleteAsync(It.IsAny<Unscramble>()), Times.Never);
        }
    }
}
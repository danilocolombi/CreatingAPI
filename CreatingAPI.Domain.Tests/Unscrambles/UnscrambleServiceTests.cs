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
    [Collection(nameof(UnscrambleTestsFixtureCollection))]
    public class UnscrambleServiceTests
    {
        private readonly Mock<IUnscrambleRepository> _repositoryMock;
        private readonly UnscrambleTestsFixture _uncrambleTestsFixture;
        private readonly UnscrumbleService _unscrambleService;

        public UnscrambleServiceTests(UnscrambleTestsFixture uncrambleTestsFixture)
        {
            _uncrambleTestsFixture = uncrambleTestsFixture;
            _repositoryMock = _uncrambleTestsFixture.GetUnscrambleRepositoryMock();
            _unscrambleService = new UnscrumbleService(_repositoryMock.Object);
        }

        [Fact(DisplayName = "Create unscramble with success, should return ResultResponse with success")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_ShouldReturnResultResponseWithSuccess()
        {
            var unscramble = _uncrambleTestsFixture.GetFakeUnscramble();
            var exercises = _uncrambleTestsFixture.GetFakeExercises();

            var result = await _unscrambleService.CreateAsync(unscramble, exercises);

            result.Success.Should().BeTrue();
            unscramble.IsValid().Should().BeTrue();
            _repositoryMock.Verify(mr => mr.CreateAsync(unscramble), Times.Once);
        }

        [Fact(DisplayName = "Update unscramble with success, should return ResultResponse with success")]
        [Trait("Category", "Update")]
        public async Task UpdateAsync_ShouldReturnResultResponseWithSuccess()
        {
            var unscramble = _uncrambleTestsFixture.GetFakeUnscramble();
            var id = _uncrambleTestsFixture.GetRandomInt();
            var exercises = _uncrambleTestsFixture.GetFakeExercises();

            var result = await _unscrambleService.UpdateAsync(id, unscramble, exercises);

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
            var unscramble = _uncrambleTestsFixture.GetFakeUnscramble();
            var exercises = _uncrambleTestsFixture.GetFakeExercises();

            var result = await _unscrambleService.UpdateAsync(invalidId, unscramble, exercises);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(mr => mr.UpdateAsync(It.IsAny<Unscramble>()), Times.Never);
        }

        [Fact(DisplayName = "Delete unscramble with success, should return ResultResponse with success")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_ShouldReturnResultResponseWithSuccess()
        {
            var id = _uncrambleTestsFixture.GetRandomInt();
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
            var result = await _unscrambleService.DeleteAsync(invalidId);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(mr => mr.DeleteAsync(It.IsAny<Unscramble>()), Times.Never);
        }

        [Fact(DisplayName = "Delete unscramble with inexistent id, should return ResultResponse with error")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_InexistentId_ShouldReturnResultResponseWithError()
        {
            var unscrambleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await _unscrambleService.DeleteAsync(_uncrambleTestsFixture.GetInexistentUnscrambleId());

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity wasn't found");
            _repositoryMock.Verify(mr => mr.DeleteAsync(It.IsAny<Unscramble>()), Times.Never);
        }
    }
}
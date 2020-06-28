using CreatingAPI.Domain.Unscrumbles;
using CreatingAPI.Domain.Unscrumbles.Interfaces;
using CreatingAPI.Domain.Unscrumbles.Services;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CreatingAPI.Domain.Tests.Unscrumbles
{
    public class UnscrumbleServiceTests
    {
        private readonly Mock<IUnscrumbleRepository> _repositoryMock;

        private const int ID_INEXISTENT_UNSCRUMBLE = 1;
        public UnscrumbleServiceTests()
        {
            _repositoryMock = new Mock<IUnscrumbleRepository>(MockBehavior.Loose);
            _repositoryMock.Setup(mr => mr.CreateUnscrumble(It.IsAny<Unscrumble>())).ReturnsAsync(UnscrumbleTestHelper.GetRandomInt());
            _repositoryMock.Setup(mr => mr.UpdateUnscrumble(It.IsAny<Unscrumble>())).ReturnsAsync(true);
            _repositoryMock.Setup(mr => mr.DeleteUnscrumble(It.IsAny<Unscrumble>())).ReturnsAsync(true);
            _repositoryMock.Setup(mr => mr.GetUnscrumble(It.Is<int>(i => i != ID_INEXISTENT_UNSCRUMBLE))).ReturnsAsync(UnscrumbleTestHelper.GetFakeUnscrumble());
            _repositoryMock.Setup(mr => mr.GetUnscrumble(It.Is<int>(i => i == ID_INEXISTENT_UNSCRUMBLE))).ReturnsAsync((Unscrumble)null);
        }

        [Fact(DisplayName = "Create unscrumble with success, should return ResultResponse with success")]
        [Trait("Category", "Create Unscrumble")]
        public async Task CreateUnscrumble_ShouldReturnResultResponseWithSuccess()
        {
            var unscrumble = UnscrumbleTestHelper.GetFakeUnscrumble();
            var exercises = UnscrumbleTestHelper.GetFakeExercises();
            var unscrumbleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrumbleService.CreateUnscrumble(unscrumble, exercises);

            result.Success.Should().BeTrue();
            unscrumble.IsValid().Should().BeTrue();
            _repositoryMock.Verify(mr => mr.CreateUnscrumble(unscrumble), Times.Once);
        }

        [Fact(DisplayName = "Create an invalid unscrumble, should return ResultResponse with error")]
        [Trait("Category", "Create Unscrumble")]
        public async Task CreateUnscrumble_InvalidUnscrumble_ShouldReturnResultResponseWithError()
        {
            var invalidUnscrumble = UnscrumbleTestHelper.GetFakeInvalidUnscrumble();
            var exercises = UnscrumbleTestHelper.GetFakeExercises();
            var unscrumbleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrumbleService.CreateUnscrumble(invalidUnscrumble, exercises);

            result.Success.Should().BeFalse();
            invalidUnscrumble.IsValid().Should().BeFalse();
            _repositoryMock.Verify(mr => mr.CreateUnscrumble(It.IsAny<Unscrumble>()), Times.Never);
        }

        [Fact(DisplayName = "Update unscrumble with success, should return ResultResponse with success")]
        [Trait("Category", "Update Unscrumble")]
        public async Task UpdateUnscrumble_ShouldReturnResultResponseWithSuccess()
        {
            var unscrumble = UnscrumbleTestHelper.GetFakeUnscrumble();
            var id = UnscrumbleTestHelper.GetRandomInt();
            var exercises = UnscrumbleTestHelper.GetFakeExercises();
            var unscrumbleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrumbleService.UpdateUnscrumble(id, unscrumble, exercises);

            result.Success.Should().BeTrue();
            unscrumble.IsValid().Should().BeTrue();
            _repositoryMock.Verify(mr => mr.UpdateUnscrumble(unscrumble), Times.Once);
        }

        [Theory(DisplayName = "Update unscrumble with invalid id, should return ResultResponse with error")]
        [Trait("Category", "Update Unscrumble")]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task UpdateUnscrumble_InvalidId_ShouldReturnResultResponseWithError(int invalidId)
        {
            var unscrumble = UnscrumbleTestHelper.GetFakeUnscrumble();
            var exercises = UnscrumbleTestHelper.GetFakeExercises();
            var unscrumbleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrumbleService.UpdateUnscrumble(invalidId, unscrumble, exercises);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(mr => mr.UpdateUnscrumble(It.IsAny<Unscrumble>()), Times.Never);
        }

        [Fact(DisplayName = "Update an invalid unscrumble, should return ResultResponse with error")]
        [Trait("Category", "Update Unscrumble")]
        public async Task UpdateUnscrumble_InvalidUnscrumble_ShouldReturnResultResponseWithError()
        {
            var invalidUnscrumble = UnscrumbleTestHelper.GetFakeInvalidUnscrumble();
            var id = UnscrumbleTestHelper.GetRandomInt();
            var exercises = UnscrumbleTestHelper.GetFakeExercises();
            var unscrumbleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrumbleService.UpdateUnscrumble(id, invalidUnscrumble, exercises);

            result.Success.Should().BeFalse();
            result.ValidationErrors.Should().NotBeEmpty();
            _repositoryMock.Verify(mr => mr.UpdateUnscrumble(It.IsAny<Unscrumble>()), Times.Never);
        }        

        [Fact(DisplayName = "Delete unscrumble with success, should return ResultResponse with success")]
        [Trait("Category", "Delete Unscrumble")]
        public async Task DeleteUnscrumble_ShouldReturnResultResponseWithSuccess()
        {
            var id = UnscrumbleTestHelper.GetRandomInt();
            var unscrumbleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrumbleService.DeleteUnscrumble(id);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(mr => mr.DeleteUnscrumble(It.IsAny<Unscrumble>()), Times.Once);
        }

        [Theory(DisplayName = "Delete unscrumble with invalid id, should return ResultResponse with error")]
        [Trait("Category", "Delete Unscrumble")]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task DeleteUnscrumble_InvalidId_ShouldReturnResultResponseWithError(int invalidId)
        {
            var unscrumbleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrumbleService.DeleteUnscrumble(invalidId);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(mr => mr.DeleteUnscrumble(It.IsAny<Unscrumble>()), Times.Never);
        }

        [Fact(DisplayName = "Delete unscrumble with inexistent id, should return ResultResponse with error")]
        [Trait("Category", "Delete Unscrumble")]
        public async Task DeleteUnscrumble_InexistentId_ShouldReturnResultResponseWithError()
        {
            var unscrumbleService = new UnscrumbleService(_repositoryMock.Object);

            var result = await unscrumbleService.DeleteUnscrumble(ID_INEXISTENT_UNSCRUMBLE);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity wasn't found");
            _repositoryMock.Verify(mr => mr.DeleteUnscrumble(It.IsAny<Unscrumble>()), Times.Never);
        }
    }
}
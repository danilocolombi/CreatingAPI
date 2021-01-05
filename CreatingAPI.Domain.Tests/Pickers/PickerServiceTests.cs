using CreatingAPI.Domain.Pickers;
using CreatingAPI.Domain.Pickers.Interfaces;
using CreatingAPI.Domain.Pickers.Services;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CreatingAPI.Domain.Tests.Pickers
{
    [Collection(nameof(PickerTestsFixtureCollection))]
    public class PickerServiceTests
    {
        private readonly PickerTestsFixture _pickerTestsFixture;
        private readonly PickerService _pickerService;
        Mock<IPickerRepository> _repositoryMock;

        public PickerServiceTests(PickerTestsFixture pickerTestsFixture)
        {
            _pickerTestsFixture = pickerTestsFixture;
            _repositoryMock = _pickerTestsFixture.GetPickerMockRepository();
            _pickerService = new PickerService(_repositoryMock.Object);

        }

        [Fact(DisplayName = "Create picker with success, should return ResultResponse with success")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_ShouldReturnResultResponseWithSuccess()
        {
            var picker = _pickerTestsFixture.GetFakePicker();
            var topics = _pickerTestsFixture.GetFakeTopics();

            var result = await _pickerService.CreateAsync(picker, topics);

            result.Success.Should().BeTrue();
            picker.IsValid().Should().BeTrue();
            _repositoryMock.Verify(rm => rm.CreateAsync(picker), Times.Once);
        }

        [Fact(DisplayName = "Create picker that throws a database error, should return ResultResponse with error")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_DatabaseError_ShouldReturnResultResponseWithError()
        {
            var picker = _pickerTestsFixture.GetPickerGeneratesDatabaseError();
            var topics = _pickerTestsFixture.GetFakeTopics();

            var result = await _pickerService.CreateAsync(picker, topics);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("There was an error while creating the activity");
            _repositoryMock.Verify(rm => rm.CreateAsync(It.IsAny<Picker>()), Times.Once);
        }

        [Fact(DisplayName = "Update picker with success, should return ResultResponse with success")]
        [Trait("Category", "Update")]
        public async Task UpdateAsync_ShouldReturnResultResponseWithSuccess()
        {
            var picker = _pickerTestsFixture.GetFakePicker();
            var topics = _pickerTestsFixture.GetFakeTopics();
            var id = _pickerTestsFixture.GetRandomInt();

            var result = await _pickerService.UpdateAsync(id, picker, topics);

            result.Success.Should().BeTrue();
            picker.IsValid().Should().BeTrue();
            _repositoryMock.Verify(rm => rm.UpdateAsync(picker), Times.Once);
        }

        [Theory(DisplayName = "Update pciker with an invalid id, should return ResultResponse with success")]
        [Trait("Category", "Update")]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task UpdateAsync_InvalidId_ShouldReturnResultResponseWithError(int invalidId)
        {
            var picker = _pickerTestsFixture.GetFakePicker();
            var topics = _pickerTestsFixture.GetFakeTopics();

            var result = await _pickerService.UpdateAsync(invalidId, picker, topics);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(rm => rm.UpdateAsync(It.IsAny<Picker>()), Times.Never);
        }

        [Fact(DisplayName = "Update picker with database error, should return ResultResponse with error")]
        [Trait("Category", "Update")]
        public async Task UpdateAsync_DatabaseError_ShouldReturnResultResponseWithError()
        {
            var picker = _pickerTestsFixture.GetPickerGeneratesDatabaseError();
            var topics = _pickerTestsFixture.GetFakeTopics();
            var id = _pickerTestsFixture.GetRandomInt();

            var result = await _pickerService.UpdateAsync(id, picker, topics);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity wasn't found");
            _repositoryMock.Verify(rm => rm.UpdateAsync(picker), Times.Once);
        }

        [Fact(DisplayName = "Delete picker with success, should return ResultResponse with success")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_ShouldReturnResultResponseWithSuccess()
        {
            var id = _pickerTestsFixture.GetRandomInt();

            var result = await _pickerService.DeleteAsync(id);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<Picker>()), Times.Once);
        }

        [Theory(DisplayName = "Delete picker with invalid id, should return ResultResponse with error")]
        [Trait("Category", "Delete")]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task DeleteAsync_InvalidId_ShouldReturnResultResponseWithError(int invalidId)
        {
            var result = await _pickerService.DeleteAsync(invalidId);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<Picker>()), Times.Never);
        }

        [Fact(DisplayName = "Delete picker with inexistent id, should return ResultResponse with error")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_InexistentId_ShouldReturnResultResponseWithError()
        {
            var result = await _pickerService.DeleteAsync(_pickerTestsFixture.GetInexistentPickerId());

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity wasn't found");
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<Picker>()), Times.Never);
        }
    }
}

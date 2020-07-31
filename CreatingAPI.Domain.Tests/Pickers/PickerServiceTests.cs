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
    public class PickerServiceTests
    {
        private readonly Mock<IPickerRepository> _repositoryMock;

        private const int ID_INEXISTENT_PICKER = 1;

        public PickerServiceTests()
        {
            _repositoryMock = new Mock<IPickerRepository>(MockBehavior.Loose);
            _repositoryMock.Setup(rm => rm.CreateAsync(It.Is<Picker>(p => p.Title != PickerTestHelper.TITLE_GENERATES_DATABASE_ERROR_PICKER)))
                                                                                .ReturnsAsync(PickerTestHelper.GetRandomInt());
            _repositoryMock.Setup(rm => rm.CreateAsync(It.Is<Picker>(p => p.Title == PickerTestHelper.TITLE_GENERATES_DATABASE_ERROR_PICKER))).ReturnsAsync(0);

            _repositoryMock.Setup(rm => rm.UpdateAsync(It.Is<Picker>(p => p.Title != PickerTestHelper.TITLE_GENERATES_DATABASE_ERROR_PICKER))).ReturnsAsync(true);
            _repositoryMock.Setup(rm => rm.UpdateAsync(It.Is<Picker>(p => p.Title == PickerTestHelper.TITLE_GENERATES_DATABASE_ERROR_PICKER))).ReturnsAsync(false);

            _repositoryMock.Setup(rm => rm.DeleteAsync(It.IsAny<Picker>())).ReturnsAsync(true);

            _repositoryMock.Setup(rm => rm.GetAsync(It.Is<int>(i => i != ID_INEXISTENT_PICKER))).ReturnsAsync(PickerTestHelper.GetFakePicker());
            _repositoryMock.Setup(rm => rm.GetAsync(It.Is<int>(i => i == ID_INEXISTENT_PICKER))).ReturnsAsync((Picker)null);
        }

        [Fact(DisplayName = "Create picker with success, should return ResultResponse with success")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_ShouldReturnResultResponseWithSuccess()
        {
            var picker = PickerTestHelper.GetFakePicker();
            var topics = PickerTestHelper.GetFakeTopics();
            var pickerService = new PickerService(_repositoryMock.Object);

            var result = await pickerService.CreateAsync(picker, topics);

            result.Success.Should().BeTrue();
            picker.IsValid().Should().BeTrue();
            _repositoryMock.Verify(rm => rm.CreateAsync(picker), Times.Once);
        }

        [Fact(DisplayName = "Create picker that throws a database error, should return ResultResponse with error")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_DatabaseError_ShouldReturnResultResponseWithError()
        {
            var picker = PickerTestHelper.GetPickerGeneratesDatabaseError();
            var topics = PickerTestHelper.GetFakeTopics();
            var pickerService = new PickerService(_repositoryMock.Object);

            var result = await pickerService.CreateAsync(picker, topics);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("There was an error while creating the activity");
            _repositoryMock.Verify(rm => rm.CreateAsync(It.IsAny<Picker>()), Times.Once);
        }

        [Fact(DisplayName = "Update picker with success, should return ResultResponse with success")]
        [Trait("Category", "Update")]
        public async Task UpdateAsync_ShouldReturnResultResponseWithSuccess()
        {
            var picker = PickerTestHelper.GetFakePicker();
            var topics = PickerTestHelper.GetFakeTopics();
            var id = PickerTestHelper.GetRandomInt();
            var pickerService = new PickerService(_repositoryMock.Object);

            var result = await pickerService.UpdateAsync(id, picker, topics);

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
            var picker = PickerTestHelper.GetFakePicker();
            var topics = PickerTestHelper.GetFakeTopics();
            var pickerService = new PickerService(_repositoryMock.Object);

            var result = await pickerService.UpdateAsync(invalidId, picker, topics);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(rm => rm.UpdateAsync(It.IsAny<Picker>()), Times.Never);
        }

        [Fact(DisplayName = "Update picker with database error, should return ResultResponse with error")]
        [Trait("Category", "Update")]
        public async Task UpdateAsync_DatabaseError_ShouldReturnResultResponseWithError()
        {
            var picker = PickerTestHelper.GetPickerGeneratesDatabaseError();
            var topics = PickerTestHelper.GetFakeTopics();
            var id = PickerTestHelper.GetRandomInt();
            var pickerService = new PickerService(_repositoryMock.Object);

            var result = await pickerService.UpdateAsync(id, picker, topics);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity wasn't found");
            _repositoryMock.Verify(rm => rm.UpdateAsync(picker), Times.Once);
        }

        [Fact(DisplayName = "Delete picker with success, should return ResultResponse with success")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_ShouldReturnResultResponseWithSuccess()
        {
            var id = PickerTestHelper.GetRandomInt();
            var pickerService = new PickerService(_repositoryMock.Object);

            var result = await pickerService.DeleteAsync(id);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<Picker>()), Times.Once);
        }

        [Theory(DisplayName = "Delete picker with invalid id, should return ResultResponse with error")]
        [Trait("Category", "Delete")]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task DeleteAsync_InvalidId_ShouldReturnResultResponseWithError(int invalidId)
        {
            var pickerService = new PickerService(_repositoryMock.Object);

            var result = await pickerService.DeleteAsync(invalidId);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity is invalid");
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<Picker>()), Times.Never);
        }

        [Fact(DisplayName = "Delete picker with inexistent id, should return ResultResponse with error")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_InexistentId_ShouldReturnResultResponseWithError()
        {
            var pickerService = new PickerService(_repositoryMock.Object);

            var result = await pickerService.DeleteAsync(ID_INEXISTENT_PICKER);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The activity wasn't found");
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<Picker>()), Times.Never);
        }
    }
}

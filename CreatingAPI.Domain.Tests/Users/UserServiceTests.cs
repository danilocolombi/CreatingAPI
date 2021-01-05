using CreatingAPI.Domain.Users;
using CreatingAPI.Domain.Users.Interfaces;
using CreatingAPI.Domain.Users.Services;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CreatingAPI.Domain.Tests.Users
{
    [Collection(nameof(UserTestsFixtureCollection))]
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly UserTestsFixture _userTestsFixture;
        private readonly UserService _userService;

        public UserServiceTests(UserTestsFixture userTestsFixture)
        {
            _userTestsFixture = userTestsFixture;
            _repositoryMock = _userTestsFixture.GetUserRepositoryMock();
            _userService = new UserService(_repositoryMock.Object);
        }

        [Fact(DisplayName = "Create an user with success, should return ResultResponse with success")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_ShouldReturnResultResponseWithSuccess()
        {
            var user = _userTestsFixture.GetFakeUser();

            var result = await _userService.CreateAsync(user);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.CreateAsync(user), Times.Once);
        }

        [Fact(DisplayName = "Change an user password with success, should return ResultResponse with success")]
        [Trait("Category", "Change Password")]
        public async Task ChangePasswordAsync_ShouldReturnResultResponseWithSuccess()
        {
            var userId = _userTestsFixture.GetRandomInt();
            var password = _userTestsFixture.GetValidPassword();

            var result = await _userService.ChangePasswordAsync(userId, password);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.UpdateAsync(It.Is<User>(u => u.Password.Characters == password)), Times.Once);
        }

        [Fact(DisplayName = "Change an inexistent user's password, should return ResultResponse with Error")]
        [Trait("Category", "Change Password")]
        public async Task ChangePasswordAsync_InexistentUser_ShouldReturnResultResponseWithError()
        {
            var password = _userTestsFixture.GetValidPassword();

            var result = await _userService.ChangePasswordAsync(_userTestsFixture.GetInexistentUserId(), password);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The user wasn't found");
            _repositoryMock.Verify(rm => rm.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact(DisplayName = "Change an user's password to an invalid one, should return ResultResponse with Error")]
        [Trait("Category", "Change Password")]
        public async Task ChangePasswordAsync_InvalidPassword_ShouldReturnResultResponseWithError()
        {
            var userId = _userTestsFixture.GetRandomInt();

            var result = await _userService.ChangePasswordAsync(userId, _userTestsFixture.GetInvalidPassword());

            result.ValidationErrors.FirstOrDefault().Message.Should().Be("invalid password");
            _repositoryMock.Verify(rm => rm.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact(DisplayName = "Delete User, should return ResultResponse with Success")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_ShouldReturnResultResponseWithSuccess()
        {
            var userId = _userTestsFixture.GetRandomInt();

            var result = await _userService.DeleteAsync(userId);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact(DisplayName = "Delete inexistent user, should return ResultResponse with Error")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_InexistentUser_ShouldReturnResultResponseWithError()
        {
            var result = await _userService.DeleteAsync(_userTestsFixture.GetInexistentUserId());

            result.Success.Should().BeFalse();
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<User>()), Times.Never);
        }
    }
}

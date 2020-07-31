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
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;

        private const int ID_INEXISTENT_USER = 1;

        public UserServiceTests()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _repositoryMock.Setup(rm => rm.CreateAsync(It.IsAny<User>())).ReturnsAsync(UserTestHelper.GetRandomInt());
            _repositoryMock.Setup(rm => rm.UpdateAsync(It.IsAny<User>())).ReturnsAsync(true);
            _repositoryMock.Setup(rm => rm.DeleteAsync(It.IsAny<User>())).ReturnsAsync(true);
            _repositoryMock.Setup(rm => rm.GetAsync(It.Is<int>(i => i == ID_INEXISTENT_USER))).ReturnsAsync((User)null);
            _repositoryMock.Setup(rm => rm.GetAsync(It.Is<int>(i => i != ID_INEXISTENT_USER))).ReturnsAsync(UserTestHelper.GetFakeUser());
        }

        [Fact(DisplayName = "Create an user with success, should return ResultResponse with success")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_ShouldReturnResultResponseWithSuccess()
        {
            var user = UserTestHelper.GetFakeUser();
            var userService = new UserService(_repositoryMock.Object);

            var result = await userService.CreateAsync(user);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.CreateAsync(user), Times.Once);
        }

        [Fact(DisplayName = "Change an user password with success, should return ResultResponse with success")]
        [Trait("Category", "Change Password")]
        public async Task ChangePasswordAsync_ShouldReturnResultResponseWithSuccess()
        {
            var userId = UserTestHelper.GetRandomInt();
            var password = UserTestHelper.VALID_PASSWORD;
            var userService = new UserService(_repositoryMock.Object);

            var result = await userService.ChangePasswordAsync(userId, password);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.UpdateAsync(It.Is<User>(u => u.Password.Characters == password)), Times.Once);
        }

        [Fact(DisplayName = "Change an inexistent user's password, should return ResultResponse with Error")]
        [Trait("Category", "Change Password")]
        public async Task ChangePasswordAsync_InexistentUser_ShouldReturnResultResponseWithError()
        {
            var password = UserTestHelper.VALID_PASSWORD;
            var userService = new UserService(_repositoryMock.Object);

            var result = await userService.ChangePasswordAsync(ID_INEXISTENT_USER, password);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The user wasn't found");
            _repositoryMock.Verify(rm => rm.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact(DisplayName = "Change an user's password to an invalid one, should return ResultResponse with Error")]
        [Trait("Category", "Change Password")]
        public async Task ChangePasswordAsync_InvalidPassword_ShouldReturnResultResponseWithError()
        {
            var userId = UserTestHelper.GetRandomInt();
            var userService = new UserService(_repositoryMock.Object);

            var result = await userService.ChangePasswordAsync(userId, UserTestHelper.INVALID_PASSWORD);

            result.ValidationErrors.FirstOrDefault().Message.Should().Be("invalid password");
            _repositoryMock.Verify(rm => rm.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact(DisplayName = "Delete User, should return ResultResponse with Success")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_ShouldReturnResultResponseWithSuccess()
        {
            var userId = UserTestHelper.GetRandomInt();
            var userService = new UserService(_repositoryMock.Object);

            var result = await userService.DeleteAsync(userId);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact(DisplayName = "Delete inexistent user, should return ResultResponse with Error")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_InexistentUser_ShouldReturnResultResponseWithError()
        {
            var userService = new UserService(_repositoryMock.Object);

            var result = await userService.DeleteAsync(ID_INEXISTENT_USER);

            result.Success.Should().BeFalse();
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<User>()), Times.Never);
        }
    }
}

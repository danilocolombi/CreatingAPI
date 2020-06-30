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
            _repositoryMock.Setup(rm => rm.CreateUser(It.IsAny<User>())).ReturnsAsync(UserTestHelper.GetRandomInt());
            _repositoryMock.Setup(rm => rm.UpdateUser(It.IsAny<User>())).ReturnsAsync(true);
            _repositoryMock.Setup(rm => rm.DeleteUser(It.IsAny<User>())).ReturnsAsync(true);
            _repositoryMock.Setup(rm => rm.GetUser(It.Is<int>(i => i == ID_INEXISTENT_USER))).ReturnsAsync((User)null);
            _repositoryMock.Setup(rm => rm.GetUser(It.Is<int>(i => i != ID_INEXISTENT_USER))).ReturnsAsync(UserTestHelper.GetFakeUser());
        }

        [Fact(DisplayName = "Create an user with success, should return ResultResponse with success")]
        [Trait("Category", "Create User")]
        public async Task CreateUser_ShouldReturnResultResponseWithSuccess()
        {
            var user = UserTestHelper.GetFakeUser();
            var userService = new UserService(_repositoryMock.Object);

            var result = await userService.CreateUser(user);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.CreateUser(user), Times.Once);
        }

        [Fact(DisplayName = "Create an invalid user, should return ResultResponse with error")]
        [Trait("Category", "Create User")]
        public async Task CreateUser_InvalidUser_ShouldReturnResultResponseWithError()
        {
            var invalidUser = UserTestHelper.GetFakeInvalidUser();
            var userService = new UserService(_repositoryMock.Object);

            var result = await userService.CreateUser(invalidUser);

            result.Success.Should().BeFalse();
            invalidUser.IsValid().Should().BeFalse();
            _repositoryMock.Verify(mr => mr.CreateUser(It.IsAny<User>()), Times.Never);
        }


        [Fact(DisplayName = "Change an user password with success, should return ResultResponse with success")]
        [Trait("Category", "Change Password")]
        public async Task ChangePassword_ShouldReturnResultResponseWithSuccess()
        {
            var userId = UserTestHelper.GetRandomInt();
            var password = UserTestHelper.VALID_PASSWORD;
            var userService = new UserService(_repositoryMock.Object);

            var result = await userService.ChangePassword(userId, password);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.UpdateUser(It.Is<User>(u => u.Password.Characters == password)), Times.Once);
        }

        [Fact(DisplayName = "Change an inexistent user's password, should return ResultResponse with Error")]
        [Trait("Category", "Change Password")]
        public async Task ChangePassword_InexistentUser_ShouldReturnResultResponseWithError()
        {
            var password = UserTestHelper.VALID_PASSWORD;
            var userService = new UserService(_repositoryMock.Object);

            var result = await userService.ChangePassword(ID_INEXISTENT_USER, password);

            result.Success.Should().BeFalse();
            result.ValidationErrors.FirstOrDefault().Message.Should().Be("The user wasn't found");
            _repositoryMock.Verify(rm => rm.UpdateUser(It.IsAny<User>()), Times.Never);
        }

        [Fact(DisplayName = "Change an user's password to an invalid one, should return ResultResponse with Error")]
        [Trait("Category", "Change Password")]
        public async Task ChangePassword_InvalidPassword_ShouldReturnResultResponseWithError()
        {
            var userId = UserTestHelper.GetRandomInt();
            var userService = new UserService(_repositoryMock.Object);

            var result = await userService.ChangePassword(userId, UserTestHelper.INVALID_PASSWORD);

            result.Success.Should().BeFalse();
            _repositoryMock.Verify(rm => rm.UpdateUser(It.IsAny<User>()), Times.Never);
        }

        [Fact(DisplayName = "Delete User, should return ResultResponse with Success")]
        [Trait("Category", "Delete User")]
        public async Task DeleteUser_ShouldReturnResultResponseWithSuccess()
        {
            var userId = UserTestHelper.GetRandomInt();
            var userService = new UserService(_repositoryMock.Object);

            var result = await userService.DeleteUser(userId);

            result.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.DeleteUser(It.IsAny<User>()), Times.Once);
        }

        [Fact(DisplayName = "Delete inexistent user, should return ResultResponse with Error")]
        [Trait("Category", "Delete User")]
        public async Task DeleteUser_InexistentUser_ShouldReturnResultResponseWithError()
        {
            var userService = new UserService(_repositoryMock.Object);

            var result = await userService.DeleteUser(ID_INEXISTENT_USER);

            result.Success.Should().BeFalse();
            _repositoryMock.Verify(rm => rm.DeleteUser(It.IsAny<User>()), Times.Never);
        }
    }
}

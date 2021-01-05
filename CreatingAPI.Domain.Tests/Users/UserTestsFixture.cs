using Bogus;
using CreatingAPI.Domain.Tests.Base;
using CreatingAPI.Domain.Users;
using CreatingAPI.Domain.Users.Interfaces;
using Moq;
using Xunit;

namespace CreatingAPI.Domain.Tests.Users
{
    [CollectionDefinition(nameof(UserTestsFixtureCollection))]
    public class UserTestsFixtureCollection : ICollectionFixture<UserTestsFixture>
    {
    }

    public class UserTestsFixture : BaseTestsFixture
    {
        private const int ID_INEXISTENT_USER = 1;
        private const string VALID_PASSWORD = "A1B2C3";
        private const string INVALID_PASSWORD = "ABC";

        public int GetInexistentUserId()
            => ID_INEXISTENT_USER;

        public string GetValidPassword()
            => VALID_PASSWORD;

        public string GetInvalidPassword()
            => INVALID_PASSWORD;

        public User GetFakeUser()
        {
            var fakeUser = new Faker<User>()
                .CustomInstantiator(f => new User(f.Person.FullName, f.Internet.Email(), "A1B2C3"));

            return fakeUser;
        }

        public string GetRandomName() => new Faker().Person.FullName;

        public Mock<IUserRepository> GetUserRepositoryMock()
        {
            var _repositoryMock = new Mock<IUserRepository>();

            _repositoryMock.Setup(rm => rm.CreateAsync(It.IsAny<User>())).ReturnsAsync(GetRandomInt());
            _repositoryMock.Setup(rm => rm.UpdateAsync(It.IsAny<User>())).ReturnsAsync(true);
            _repositoryMock.Setup(rm => rm.DeleteAsync(It.IsAny<User>())).ReturnsAsync(true);
            _repositoryMock.Setup(rm => rm.GetAsync(It.Is<int>(i => i == ID_INEXISTENT_USER))).ReturnsAsync((User)null);
            _repositoryMock.Setup(rm => rm.GetAsync(It.Is<int>(i => i != ID_INEXISTENT_USER))).ReturnsAsync(GetFakeUser());

            return _repositoryMock;
        }
    }
}

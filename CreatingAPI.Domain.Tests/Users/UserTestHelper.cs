using Bogus;
using CreatingAPI.Domain.Tests.Utils;
using CreatingAPI.Domain.Users;

namespace CreatingAPI.Domain.Tests.Users
{
    public class UserTestHelper : TestHelper
    {
        public const string VALID_PASSWORD = "A1B2C3";
        public const string INVALID_PASSWORD = "ABC";

        public static User GetFakeUser()
        {
            var fakeUser = new Faker<User>()
                .CustomInstantiator(f => new User(f.Person.FullName, f.Internet.Email(), "A1B2C3"));

            return fakeUser;
        }

        public static string GetRandomName() => new Faker().Person.FullName;
    }
}

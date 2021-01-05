using FluentAssertions;
using System;
using Xunit;

namespace CreatingAPI.Domain.Tests.Users
{
    [Collection(nameof(UserTestsFixtureCollection))]
    public class UserTests
    {
        private readonly UserTestsFixture _userTestsFixture;

        public UserTests(UserTestsFixture userTestsFixture)
        {
            _userTestsFixture = userTestsFixture;
        }

        [Fact(DisplayName = "Set Password, should return true")]
        [Trait("Category", "Set Password")]
        public void SetPassword_ShouldReturnTrue()
        {
            var user = _userTestsFixture.GetFakeUser();

            user.SetPassword(_userTestsFixture.GetValidPassword());

            user.ValidationErrors.Should().BeEmpty();
            user.Password.Characters.Should().Be(_userTestsFixture.GetValidPassword());
        }

        [Fact(DisplayName = "Set Password, should return false")]
        [Trait("Category", "Set Password")]
        public void SetPassword_ShouldReturnFalse()
        {
            var user = _userTestsFixture.GetFakeUser();
            var invalidPassword = "";

            Action act = () => user.SetPassword(invalidPassword);

            act.Should().Throw<Exception>().WithMessage("invalid password");
            user.Password.Characters.Should().NotBe(invalidPassword);
        }
    }
}

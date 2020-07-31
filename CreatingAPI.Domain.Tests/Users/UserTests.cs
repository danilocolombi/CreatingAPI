using FluentAssertions;
using System;
using Xunit;

namespace CreatingAPI.Domain.Tests.Users
{
    public class UserTests
    {
        [Fact(DisplayName = "Set Password, should return true")]
        [Trait("Category", "Set Password")]
        public void SetPassword_ShouldReturnTrue()
        {
            var user = UserTestHelper.GetFakeUser();

            user.SetPassword(UserTestHelper.VALID_PASSWORD);

            user.ValidationErrors.Should().BeEmpty();
            user.Password.Characters.Should().Be(UserTestHelper.VALID_PASSWORD);
        }

        [Fact(DisplayName = "Set Password, should return false")]
        [Trait("Category", "Set Password")]
        public void SetPassword_ShouldReturnFalse()
        {
            var user = UserTestHelper.GetFakeUser();
            var invalidPassword = "";

            Action act = () => user.SetPassword(invalidPassword);

            act.Should().Throw<Exception>().WithMessage("invalid password");
            user.Password.Characters.Should().NotBe(invalidPassword);
        }
    }
}

using FluentAssertions;
using Xunit;

namespace CreatingAPI.Domain.Tests.Users
{
    public class UserTests
    {

        [Fact(DisplayName = "Set Name, should return true")]
        [Trait("Category", "Set Name")]
        public void SetName_ShouldReturnTrue()
        {
            var user = UserTestHelper.GetFakeUser();
            var newName = UserTestHelper.GetRandomName();

            var result = user.SetName(newName);

            result.Should().BeTrue();
            user.ValidationErrors.Should().BeEmpty();
            user.Name.Should().Be(newName);
        }

        [Theory(DisplayName = "Set Name, should return false")]
        [Trait("Category", "Set Name")]
        [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec nec consectetur nulla. " +
         "Fusce venenatis finibus lorem, a volutpat erat dictum ullamcorper. Aliquam velit quam, convallis eget facilisis id, cursus vel ex")]
        [InlineData("")]
        [InlineData(" ")]
        public void SetName_ShouldReturnFalse(string invalidName)
        {
            var user = UserTestHelper.GetFakeUser();

            var result = user.SetName(invalidName);

            result.Should().BeFalse();
            user.ValidationErrors.Should().NotBeEmpty();
            user.Name.Should().NotBe(invalidName);
        }

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

             user.SetPassword(invalidPassword);

            user.ValidationErrors.Should().NotBeEmpty();
            user.Password.Characters.Should().NotBe(invalidPassword);
        }
    }
}

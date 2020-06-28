using FluentAssertions;
using Xunit;

namespace CreatingAPI.Domain.Tests.Activities
{
    public class ActivityTests
    {
        [Fact(DisplayName = "Set title, should return true")]
        [Trait("Category", "Set Title")]
        public void SetTitle_ShouldReturnTrue()
        {
            var activity = ActivityTestHelper.GetFakeActivity();
            var newTitle = ActivityTestHelper.GetRandomSentece();

            var result = activity.SetTitle(newTitle);

            result.Should().BeTrue();
            activity.ValidationErrors.Should().BeEmpty();
            activity.Title.Should().Be(newTitle);
        }

        [Theory(DisplayName = "Set Title, should return false")]
        [Trait("Category", "Set Title")]
        [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec nec consectetur nulla. " +
         "Fusce venenatis finibus lorem, a volutpat erat dictum ullamcorper. Aliquam velit quam, convallis eget facilisis id, cursus vel ex")]
        [InlineData("")]
        [InlineData(" ")]
        public void SetTitle_ShouldReturnFalse(string title)
        {
            var activity = ActivityTestHelper.GetFakeActivity();

            var result = activity.SetTitle(title);

            result.Should().BeFalse();
            activity.ValidationErrors.Should().NotBeEmpty();
            activity.Title.Should().NotBe(title);
        }

        [Fact(DisplayName = "Set User Id, should return true")]
        [Trait("Category", "Set User Id")]
        public void SetUserId_ShouldReturnTrue()
        {
            var activity = ActivityTestHelper.GetFakeActivity();
            var userId = ActivityTestHelper.GetRandomInt();

            var result = activity.SetUserId(userId);

            result.Should().BeTrue();
            activity.ValidationErrors.Should().BeEmpty();
            activity.UserId.Should().Be(userId);
        }

        [Theory(DisplayName = "Set User Id, should return false")]
        [Trait("Category", "Set User Id")]
        [InlineData(-1)]
        [InlineData(0)]
        public void SetUserId_ShouldReturnFalse(int invalidUserId)
        {
            var activity = ActivityTestHelper.GetFakeActivity();

            var result = activity.SetUserId(invalidUserId);

            result.Should().BeFalse();
            activity.ValidationErrors.Should().NotBeEmpty();
            activity.UserId.Should().NotBe(invalidUserId);
        }

        [Fact(DisplayName = "Set Is Public, should return true")]
        [Trait("Category", "Set Is Public")]
        public void SetIsPublic_ShouldReturnTrue()
        {
            var activity = ActivityTestHelper.GetFakeActivity();
            var isPublic = ActivityTestHelper.GetRandomBool();

            activity.SetIsPublic(isPublic);

            activity.ValidationErrors.Should().BeEmpty();
            activity.IsPublic.Should().Be(isPublic);
        }
    }
}

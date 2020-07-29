using FluentAssertions;
using Xunit;

namespace CreatingAPI.Domain.Tests.Pickers
{
    public class PickerTests
    {
        [Fact(DisplayName = "Add topics, should return true")]
        [Trait("Category", "Add Topics")]
        public void AddTopics_ShouldReturnTrue()
        {
            var picker = PickerTestHelper.GetFakePicker();
            var topics = PickerTestHelper.GetFakeTopics();

            var result = picker.AddTopics(topics);

            result.Should().BeTrue();
            picker.Topics.Should().BeEquivalentTo(topics);
        }

        [Fact(DisplayName = "Add topic with invalid topics, should return false")]
        [Trait("Category", "Add Topics")]
        public void AddTopics_InvalidTopics_ShouldReturnFalse()
        {
            var picker = PickerTestHelper.GetFakePicker();
            var topics = PickerTestHelper.GetFakeInvalidTopics();

            var result = picker.AddTopics(topics);

            result.Should().BeFalse();
        }
    }
}

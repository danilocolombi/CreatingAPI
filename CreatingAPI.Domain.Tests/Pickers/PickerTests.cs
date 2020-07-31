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
    }
}

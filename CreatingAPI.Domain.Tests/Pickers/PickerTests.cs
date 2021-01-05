using FluentAssertions;
using Xunit;

namespace CreatingAPI.Domain.Tests.Pickers
{
    [Collection(nameof(PickerTestsFixtureCollection))]
    public class PickerTests
    {
        private readonly PickerTestsFixture _pickerTestsFixture;

        public PickerTests(PickerTestsFixture pickerTestsFixture)
        {
            _pickerTestsFixture = pickerTestsFixture;
        }

        [Fact(DisplayName = "Add topics, should return true")]
        [Trait("Category", "Add Topics")]
        public void AddTopics_ShouldReturnTrue()
        {
            var picker = _pickerTestsFixture.GetFakePicker();
            var topics = _pickerTestsFixture.GetFakeTopics();

            var result = picker.AddTopics(topics);

            result.Should().BeTrue();
            picker.Topics.Should().BeEquivalentTo(topics);
        }
    }
}

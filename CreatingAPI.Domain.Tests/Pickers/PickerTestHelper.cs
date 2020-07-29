using Bogus;
using CreatingAPI.Domain.Pickers;
using CreatingAPI.Domain.Tests.Utils;
using System.Collections.Generic;

namespace CreatingAPI.Domain.Tests.Pickers
{
    public class PickerTestHelper : TestHelper
    {
        public static Picker GetFakePicker()
        {
            var fakePicker = new Faker<Picker>()
                .CustomInstantiator(f => new Picker(f.Lorem.Sentence(), f.Random.Int(1), f.Random.Bool()));

            return fakePicker;
        }

        public static Picker GetFakeInvalidPicker()
        {
            var fakePicker = new Faker<Picker>()
                .CustomInstantiator(f => new Picker(string.Empty, f.Random.Int(1), f.Random.Bool()));

            return fakePicker;
        }

        public static IEnumerable<PickerTopic> GetFakeTopics()
        {
            return new Faker<PickerTopic>()
                .CustomInstantiator(f => new PickerTopic(f.Lorem.Sentence())).Generate(10);
        }

        public static IEnumerable<PickerTopic> GetFakeInvalidTopics()
        {
            return new Faker<PickerTopic>()
                .CustomInstantiator(f => new PickerTopic(string.Empty)).Generate(10);
        }
    }
}

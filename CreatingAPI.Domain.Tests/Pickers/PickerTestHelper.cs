using Bogus;
using CreatingAPI.Domain.Pickers;
using CreatingAPI.Domain.Tests.Utils;
using System.Collections.Generic;

namespace CreatingAPI.Domain.Tests.Pickers
{
    public class PickerTestHelper : TestHelper
    {
        public const string TITLE_GENERATES_DATABASE_ERROR_PICKER = "DATABASE ERROR";

        public static Picker GetFakePicker()
        {
            var fakePicker = new Faker<Picker>()
                .CustomInstantiator(f => new Picker(f.Lorem.Sentence(), f.Random.Int(1), f.Random.Bool()));

            return fakePicker;
        }

        public static Picker GetPickerGeneratesDatabaseError()
        {
            var fakePicker = new Faker<Picker>()
                .CustomInstantiator(f => new Picker(TITLE_GENERATES_DATABASE_ERROR_PICKER, f.Random.Int(1), f.Random.Bool()));

            return fakePicker;
        }

        public static IEnumerable<PickerTopic> GetFakeTopics()
        {
            return new Faker<PickerTopic>()
                .CustomInstantiator(f => new PickerTopic(f.Lorem.Sentence())).Generate(10);
        }
    }
}

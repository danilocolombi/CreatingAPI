using Bogus;
using CreatingAPI.Domain.Tests.Utils;
using CreatingAPI.Domain.Unscrumbles;
using System.Collections.Generic;

namespace CreatingAPI.Domain.Tests.Unscrumbles
{
    public class UnscrumbleTestHelper : TestHelper
    {
        public static Unscrumble GetFakeUnscrumble()
        {
            var fakeUnscrumble = new Faker<Unscrumble>()
                .CustomInstantiator(f => new Unscrumble(f.Lorem.Sentence(), f.Random.Int(1), f.Random.Bool()));

            return fakeUnscrumble;
        }

        public static Unscrumble GetFakeInvalidUnscrumble()
        {
            var fakeUnscrumble = new Faker<Unscrumble>()
                .CustomInstantiator(f => new Unscrumble(f.Lorem.Paragraph(2), f.Random.Int(-1000, -1), f.Random.Bool()));

            return fakeUnscrumble;
        }

        public static IEnumerable<Exercise> GetFakeExercises()
        {
            int uniquePosition = 0;

            return new Faker<Exercise>()
                .CustomInstantiator(f => new Exercise(f.Lorem.Sentence(), uniquePosition++)).Generate(10);
        }
    }
}

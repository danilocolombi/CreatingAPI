using Bogus;
using CreatingAPI.Domain.Tests.Utils;
using CreatingAPI.Domain.Unscrambles;
using System.Collections.Generic;

namespace CreatingAPI.Domain.Tests.Unscrambles
{
    public class UnscrambleTestHelper : TestHelper
    {
        public static Unscramble GetFakeUnscramble()
        {
            var fakeUnscramble = new Faker<Unscramble>()
                .CustomInstantiator(f => new Unscramble(f.Lorem.Sentence(), f.Random.Int(1), f.Random.Bool()));

            return fakeUnscramble;
        }

        public static Unscramble GetFakeInvalidUnscramble()
        {
            var fakeUnscramble = new Faker<Unscramble>()
                .CustomInstantiator(f => new Unscramble(f.Lorem.Paragraph(2), f.Random.Int(-1000, -1), f.Random.Bool()));

            return fakeUnscramble;
        }

        public static IEnumerable<Exercise> GetFakeExercises()
        {
            int uniquePosition = 0;

            return new Faker<Exercise>()
                .CustomInstantiator(f => new Exercise(f.Lorem.Sentence(), uniquePosition++)).Generate(10);
        }
    }
}

using FluentAssertions;
using System.Linq;
using Xunit;

namespace CreatingAPI.Domain.Tests.Unscrambles
{
    [Collection(nameof(UnscrambleTestsFixtureCollection))]
    public class UnscrambleTests
    {
        private readonly UnscrambleTestsFixture _uncrambleTestsFixture;

        public UnscrambleTests(UnscrambleTestsFixture uncrambleTestsFixture)
        {
            _uncrambleTestsFixture = uncrambleTestsFixture;
        }

        [Fact(DisplayName = "Add exercises, should return true")]
        [Trait("Category", "Add Exercises")]
        public void AddExercises_ShouldReturnTrue()
        {
            var unscramble = _uncrambleTestsFixture.GetFakeUnscramble();
            var exercises = _uncrambleTestsFixture.GetFakeExercises();

            var result = unscramble.AddExercises(exercises);

            result.Should().BeTrue();
            unscramble.Exercises.Should().BeEquivalentTo(exercises);
        }

        [Fact(DisplayName = "Add exercises with an invalid exercise, should return true")]
        [Trait("Category", "Add Exercises")]
        public void AddExercises_InvalidExercise_ShouldReturnFalse()
        {
            var unscramble = _uncrambleTestsFixture.GetFakeUnscramble();
            var exercises = _uncrambleTestsFixture.GetFakeExercises();
            exercises.FirstOrDefault().SetDescription(string.Empty);

            var result = unscramble.AddExercises(exercises);

            result.Should().BeFalse();
            unscramble.Exercises.Should().NotBeEquivalentTo(exercises);
        }

        [Fact(DisplayName = "Get shuffled exercises, should return a list of ShuffledExercises")]
        [Trait("Category", "Get Shuffled Exercises")]
        public void GetShuffledExercises_ShouldReturnAListOfShuffledExercises()
        {
            var unscramble = _uncrambleTestsFixture.GetFakeUnscramble();
            var randomizeExercisesOrder = _uncrambleTestsFixture.GetRandomBool();

            var result = unscramble.GetShuffledExercises(randomizeExercisesOrder);

            result.Should().NotBeNull();
            result.Should().OnlyContain(se => se.Part1 != null || se.Part2 != null && se.Part3 != null && se.Part4 != null);
        }
    }
}

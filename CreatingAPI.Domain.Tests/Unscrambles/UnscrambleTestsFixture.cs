using Bogus;
using CreatingAPI.Domain.Tests.Base;
using CreatingAPI.Domain.Unscrambles;
using CreatingAPI.Domain.Unscrambles.Interfaces;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CreatingAPI.Domain.Tests.Unscrambles
{
    [CollectionDefinition(nameof(UnscrambleTestsFixtureCollection))]
    public class UnscrambleTestsFixtureCollection : ICollectionFixture<UnscrambleTestsFixture>
    {
    }

    public class UnscrambleTestsFixture : BaseTestsFixture
    {
        private const int ID_INEXISTENT_UNSCRAMBLE = 1;

        public int GetInexistentUnscrambleId()
             => ID_INEXISTENT_UNSCRAMBLE;

        public Unscramble GetFakeUnscramble()
        {
            var fakeUnscramble = new Faker<Unscramble>()
                .CustomInstantiator(f => new Unscramble(f.Lorem.Sentence(), f.Random.Int(1), f.Random.Bool()));

            fakeUnscramble.RuleFor(u => u.Exercises, f => GetFakeExercises());

            return fakeUnscramble;
        }

        public IEnumerable<Exercise> GetFakeExercises()
        {
            int uniquePosition = 1;

            return new Faker<Exercise>()
                .CustomInstantiator(f => new Exercise(f.Lorem.Sentence(), uniquePosition++)).Generate(10);
        }

        public Mock<IUnscrambleRepository> GetUnscrambleRepositoryMock()
        {
            var _repositoryMock = new Mock<IUnscrambleRepository>(MockBehavior.Loose);
            _repositoryMock.Setup(mr => mr.CreateAsync(It.IsAny<Unscramble>())).ReturnsAsync(GetRandomInt());
            _repositoryMock.Setup(mr => mr.UpdateAsync(It.IsAny<Unscramble>())).ReturnsAsync(true);
            _repositoryMock.Setup(mr => mr.DeleteAsync(It.IsAny<Unscramble>())).ReturnsAsync(true);
            _repositoryMock.Setup(mr => mr.GetAsync(It.Is<int>(i => i != ID_INEXISTENT_UNSCRAMBLE))).ReturnsAsync(GetFakeUnscramble());
            _repositoryMock.Setup(mr => mr.GetAsync(It.Is<int>(i => i == ID_INEXISTENT_UNSCRAMBLE))).ReturnsAsync((Unscramble)null);

            return _repositoryMock;
        }
    }
}

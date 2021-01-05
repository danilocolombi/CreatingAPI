using Bogus;
using CreatingAPI.Domain.Tests.Base;
using CreatingAPI.Domain.TicTacToes;
using CreatingAPI.Domain.TicTacToes.Interfaces;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CreatingAPI.Domain.Tests.TicTacToes
{
    [CollectionDefinition(nameof(TicTacToeTestsFixtureCollection))]
    public class TicTacToeTestsFixtureCollection : ICollectionFixture<TicTacToeTestsFixture>
    {
    }

    public class TicTacToeTestsFixture : BaseTestsFixture
    {
        private const int ID_INEXISTENT_TIC_TAC_TOE = 1;

        public int GetInexistentTicTacToeId()
            => ID_INEXISTENT_TIC_TAC_TOE;

        public TicTacToe GetFakeTicTacToe()
        {
            var fakeTicTacToe = new Faker<TicTacToe>()
                .CustomInstantiator(f => new TicTacToe(f.Lorem.Sentence(), f.Random.Int(1), f.Random.Bool()));

            return fakeTicTacToe;
        }

        public IEnumerable<TicTacToeSquare> GetFakeTicTacToeSquares()
        {
            int uniquePosition = 1;

            return new Faker<TicTacToeSquare>()
                .CustomInstantiator(f => new TicTacToeSquare(f.Lorem.Sentence(), uniquePosition++)).Generate(9);
        }

        public IEnumerable<TicTacToeSquare> GetFakeInvalidTicTacToeSquares()
        {
            return new Faker<TicTacToeSquare>()
                .CustomInstantiator(f => new TicTacToeSquare(f.Lorem.Sentence(), GetRandomInt())).Generate(9);
        }

        public Mock<ITicTacToeRepository> GetTicTacToeRepositoryMock()
        {
            var repositoryMock = new Mock<ITicTacToeRepository>(MockBehavior.Loose);
            repositoryMock.Setup(rm => rm.CreateAsync(It.IsAny<TicTacToe>())).ReturnsAsync(GetRandomInt());
            repositoryMock.Setup(rm => rm.UpdateAsync(It.IsAny<TicTacToe>())).ReturnsAsync(true);
            repositoryMock.Setup(rm => rm.DeleteAsync(It.IsAny<TicTacToe>())).ReturnsAsync(true);
            repositoryMock.Setup(rm => rm.GetAsync(It.Is<int>(i => i != ID_INEXISTENT_TIC_TAC_TOE))).ReturnsAsync(GetFakeTicTacToe());
            repositoryMock.Setup(rm => rm.GetAsync(It.Is<int>(i => i == ID_INEXISTENT_TIC_TAC_TOE))).ReturnsAsync((TicTacToe)null);

            return repositoryMock;
        }
    }
}

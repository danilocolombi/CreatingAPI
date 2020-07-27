using Bogus;
using CreatingAPI.Domain.Tests.Utils;
using CreatingAPI.Domain.TicTacToes;
using System.Collections.Generic;

namespace CreatingAPI.Domain.Tests.TicTacToes
{
    public class TicTacToeTestHelper : TestHelper
    {
        public static TicTacToe GetFakeTicTacToe()
        {
            var fakeTicTacToe = new Faker<TicTacToe>()
                .CustomInstantiator(f => new TicTacToe(f.Lorem.Sentence(), f.Random.Int(1), f.Random.Bool()));

            return fakeTicTacToe;
        }

        public static TicTacToe GetFakeInvalidTicTacToe()
        {
            var fakeTicTacToe = new Faker<TicTacToe>()
                .CustomInstantiator(f => new TicTacToe(string.Empty, f.Random.Int(1), f.Random.Bool()));

            return fakeTicTacToe;
        }

        public static IEnumerable<TicTacToeSquare> GetFakeTicTacToeSquares()
        {
            int uniquePosition = 1;

            return new Faker<TicTacToeSquare>()
                .CustomInstantiator(f => new TicTacToeSquare(f.Lorem.Sentence(), uniquePosition++)).Generate(9);
        }

        public static IEnumerable<TicTacToeSquare> GetFakeInvalidTicTacToeSquares()
        {
            return new Faker<TicTacToeSquare>()
                .CustomInstantiator(f => new TicTacToeSquare(f.Lorem.Sentence(), GetRandomInt())).Generate(9);
        }
    }
}

using Bogus;

namespace CreatingAPI.Domain.Tests.Base
{
    public abstract class BaseTestsFixture
    {
        public string GetRandomSentece() => new Faker().Lorem.Sentence();
        public int GetRandomInt() => new Faker().Random.Int(2);
        public bool GetRandomBool() => new Faker().Random.Bool();
    }
}

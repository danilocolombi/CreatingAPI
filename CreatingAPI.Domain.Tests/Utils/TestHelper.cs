using Bogus;

namespace CreatingAPI.Domain.Tests.Utils
{
    public class TestHelper
    {
        public static string GetRandomSentece() => new Faker().Lorem.Sentence();
        public static int GetRandomInt() => new Faker().Random.Int(2);
        public static bool GetRandomBool() => new Faker().Random.Bool();
    }
}

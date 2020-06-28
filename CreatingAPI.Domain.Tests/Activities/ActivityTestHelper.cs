using Bogus;
using CreatingAPI.Domain.Activities;
using CreatingAPI.Domain.Tests.Utils;

namespace CreatingAPI.Domain.Tests.Activities
{
    public class ActivityTestHelper : TestHelper
    {
        public static Activity GetFakeActivity()
        {
            var fakeActivity = new Faker<Activity>()
                .CustomInstantiator(f => new Activity(f.Lorem.Sentence(), f.Random.Int(1), f.Random.Bool()));

            return fakeActivity;
        }
    }
}

using Bogus;
using CreatingAPI.Domain.Activities;
using CreatingAPI.Domain.Bookmarks;
using CreatingAPI.Domain.Tests.Utils;

namespace CreatingAPI.Domain.Tests.Bookmarks
{
    public class BookmarkTestHelper : TestHelper
    {
        public static Bookmark GetFakeBookmark()
        {
            var fakeBookmark = new Faker<Bookmark>()
                .CustomInstantiator(b => new Bookmark(b.Random.Int(1), b.Random.Int(1), b.PickRandom<KindOfActivity>()));

            return fakeBookmark;
        }

        public static Bookmark GetFakeInvalidBookmark()
        {
            var fakeBookmark = new Faker<Bookmark>()
                .CustomInstantiator(b => new Bookmark(b.Random.Int(1), b.Random.Int(1), (KindOfActivity) b.Random.Int(1)));

            return fakeBookmark;
        }
    }
}

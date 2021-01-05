using Bogus;
using CreatingAPI.Domain.Activities;
using CreatingAPI.Domain.Bookmarks;
using CreatingAPI.Domain.Bookmarks.Interfaces;
using CreatingAPI.Domain.Tests.Base;
using Moq;
using System;
using Xunit;

namespace CreatingAPI.Domain.Tests.Bookmarks
{
    [CollectionDefinition(nameof(BookmarkTestsFixtureCollection))]
    public class BookmarkTestsFixtureCollection : ICollectionFixture<BookmarkTestsFixture>
    {
    }
    public class BookmarkTestsFixture : BaseTestsFixture, IDisposable
    {
        private const int ID_INEXISTENT_BOOKMARK = 1;

        public int GetInexistentBoomarkId()
            => ID_INEXISTENT_BOOKMARK;

        public Bookmark GetFakeBookmark()
        {
            var fakeBookmark = new Faker<Bookmark>()
                .CustomInstantiator(b => new Bookmark(b.Random.Int(1), b.Random.Int(1), b.PickRandom<KindOfActivity>()));

            return fakeBookmark;
        }

        public Bookmark GetFakeInvalidBookmark()
        {
            var fakeBookmark = new Faker<Bookmark>()
                .CustomInstantiator(b => new Bookmark(b.Random.Int(1), b.Random.Int(1), (KindOfActivity)b.Random.Int(1)));

            return fakeBookmark;
        }

        public Mock<IBookmarkRepository> GetBookmarkRepositoryMock()
        {
            var repositoryMock = new Mock<IBookmarkRepository>(MockBehavior.Loose);

            repositoryMock.Setup(rm => rm.CreateAsync(It.IsAny<Bookmark>())).ReturnsAsync(GetRandomInt());
            repositoryMock.Setup(rm => rm.GetAsync(It.Is<int>(i => i != ID_INEXISTENT_BOOKMARK))).ReturnsAsync(GetFakeBookmark());
            repositoryMock.Setup(rm => rm.GetAsync(It.Is<int>(i => i == ID_INEXISTENT_BOOKMARK))).ReturnsAsync((Bookmark)null);
            repositoryMock.Setup(rm => rm.DeleteAsync(It.IsAny<Bookmark>())).ReturnsAsync(true);

            return repositoryMock;
        }

        public void Dispose()
        {
        }
    }
}

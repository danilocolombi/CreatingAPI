using CreatingAPI.Domain.Bookmarks;
using CreatingAPI.Domain.Bookmarks.Interfaces;
using Moq;

namespace CreatingAPI.Domain.Tests.Bookmarks
{
    public class BookmarkServiceTests
    {
        private readonly Mock<IBookmarkRepository> _repositoryMock;

        private const int ID_INEXISTENT_BOOKMARK = 1;

        public BookmarkServiceTests()
        {
            _repositoryMock = new Mock<IBookmarkRepository>(MockBehavior.Loose);
            _repositoryMock.Setup(rm => rm.CreateBookmark(It.IsAny<Bookmark>())).ReturnsAsync(BookmarkTestHelper.GetRandomInt());
            _repositoryMock.Setup(rm => rm.GetBookmark(It.Is<int>(i => i != ID_INEXISTENT_BOOKMARK))).ReturnsAsync(BookmarkTestHelper.GetFakeBookmark());
            _repositoryMock.Setup(rm => rm.GetBookmark(It.Is<int>(i => i == ID_INEXISTENT_BOOKMARK))).ReturnsAsync((Bookmark)null);
            _repositoryMock.Setup(rm => rm.DeleteBookmark(It.IsAny<Bookmark>())).ReturnsAsync(true);
        }
    }
}

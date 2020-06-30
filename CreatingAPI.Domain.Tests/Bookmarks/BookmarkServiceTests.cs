using CreatingAPI.Domain.Bookmarks;
using CreatingAPI.Domain.Bookmarks.Interfaces;
using CreatingAPI.Domain.Bookmarks.Services;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

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

        [Fact(DisplayName = "Create bookmark with success, should return ResultResponse with success")]
        [Trait("Category", "Create Bookmark")]
        public async Task CreateBookmark_ShouldReturnResultResponseWithSuccess()
        {
            var bookmark = BookmarkTestHelper.GetFakeBookmark();
            var bookmarkService = new BookmarkService(_repositoryMock.Object);

            var resultBookmarkCreated = await bookmarkService.CreateBookmark(bookmark);

            resultBookmarkCreated.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.CreateBookmark(bookmark), Times.Once);
        }

        [Fact(DisplayName = "Create an invalid bookmark, should return ResultResponse with error")]
        [Trait("Category", "Create Bookmark")]
        public async Task CreateBookmark_InvalidBookmark_ShouldReturnResultResponseWithError()
        {
            var invalidBookmark = BookmarkTestHelper.GetFakeInvalidBookmark();
            var bookmarkService = new BookmarkService(_repositoryMock.Object);

            var resultBookmarkCreated = await bookmarkService.CreateBookmark(invalidBookmark);

            resultBookmarkCreated.Success.Should().BeFalse();
            _repositoryMock.Verify(rm => rm.CreateBookmark(It.IsAny<Bookmark>()), Times.Never);
        }

        [Fact(DisplayName = "Delete bookmark with success, should return ResultResponse with success")]
        [Trait("Category", "Delete Bookmark")]
        public async Task DeleteBookmark_ShouldReturnResultResponseWithSuccess()
        {
            var bookmarkId = BookmarkTestHelper.GetRandomInt();
            var bookmarkService = new BookmarkService(_repositoryMock.Object);

            var resultBookmarkDeleted = await bookmarkService.DeleteBookmark(bookmarkId);

            resultBookmarkDeleted.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.DeleteBookmark(It.IsAny<Bookmark>()), Times.Once);
        }

        [Fact(DisplayName = "Delete bookmark with inexistent id, should return ResultResponse with error")]
        [Trait("Category", "Delete Bookmark")]
        public async Task DeleteBookmark_InexistentId_ShouldReturnResultResponseWithError()
        {
            var bookmarkService = new BookmarkService(_repositoryMock.Object);

            var resultBookmarkDeleted = await bookmarkService.DeleteBookmark(ID_INEXISTENT_BOOKMARK);

            resultBookmarkDeleted.Success.Should().BeFalse();
            resultBookmarkDeleted.ValidationErrors.FirstOrDefault().Message.Should().Be("The bookmark wasn't found");
            _repositoryMock.Verify(rm => rm.DeleteBookmark(It.IsAny<Bookmark>()), Times.Never);
        }
    }
}

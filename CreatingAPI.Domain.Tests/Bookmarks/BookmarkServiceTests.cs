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
            _repositoryMock.Setup(rm => rm.CreateAsync(It.IsAny<Bookmark>())).ReturnsAsync(BookmarkTestHelper.GetRandomInt());
            _repositoryMock.Setup(rm => rm.GetAsync(It.Is<int>(i => i != ID_INEXISTENT_BOOKMARK))).ReturnsAsync(BookmarkTestHelper.GetFakeBookmark());
            _repositoryMock.Setup(rm => rm.GetAsync(It.Is<int>(i => i == ID_INEXISTENT_BOOKMARK))).ReturnsAsync((Bookmark)null);
            _repositoryMock.Setup(rm => rm.DeleteAsync(It.IsAny<Bookmark>())).ReturnsAsync(true);
        }

        [Fact(DisplayName = "Create bookmark with success, should return ResultResponse with success")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_ShouldReturnResultResponseWithSuccess()
        {
            var bookmark = BookmarkTestHelper.GetFakeBookmark();
            var bookmarkService = new BookmarkService(_repositoryMock.Object);

            var resultBookmarkCreated = await bookmarkService.CreateAsync(bookmark);

            resultBookmarkCreated.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.CreateAsync(bookmark), Times.Once);
        }

        [Fact(DisplayName = "Create an invalid bookmark, should return ResultResponse with error")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_InvalidBookmark_ShouldReturnResultResponseWithError()
        {
            var invalidBookmark = BookmarkTestHelper.GetFakeInvalidBookmark();
            var bookmarkService = new BookmarkService(_repositoryMock.Object);

            var resultBookmarkCreated = await bookmarkService.CreateAsync(invalidBookmark);

            resultBookmarkCreated.Success.Should().BeFalse();
            _repositoryMock.Verify(rm => rm.CreateAsync(It.IsAny<Bookmark>()), Times.Never);
        }

        [Fact(DisplayName = "Delete bookmark with success, should return ResultResponse with success")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_ShouldReturnResultResponseWithSuccess()
        {
            var bookmarkId = BookmarkTestHelper.GetRandomInt();
            var bookmarkService = new BookmarkService(_repositoryMock.Object);

            var resultBookmarkDeleted = await bookmarkService.DeleteAsync(bookmarkId);

            resultBookmarkDeleted.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<Bookmark>()), Times.Once);
        }

        [Fact(DisplayName = "Delete bookmark with inexistent id, should return ResultResponse with error")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_InexistentId_ShouldReturnResultResponseWithError()
        {
            var bookmarkService = new BookmarkService(_repositoryMock.Object);

            var resultBookmarkDeleted = await bookmarkService.DeleteAsync(ID_INEXISTENT_BOOKMARK);

            resultBookmarkDeleted.Success.Should().BeFalse();
            resultBookmarkDeleted.ValidationErrors.FirstOrDefault().Message.Should().Be("The bookmark wasn't found");
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<Bookmark>()), Times.Never);
        }
    }
}

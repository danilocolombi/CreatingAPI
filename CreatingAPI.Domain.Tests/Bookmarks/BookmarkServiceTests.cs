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
    [Collection(nameof(BookmarkTestsFixtureCollection))]
    public class BookmarkServiceTests
    {
        private readonly BookmarkTestsFixture _bookmarkTestsFixture;
        private readonly BookmarkService _bookmarkService;
        private readonly Mock<IBookmarkRepository> _repositoryMock;

        public BookmarkServiceTests(BookmarkTestsFixture bookmarkTestsFixture)
        {
            _bookmarkTestsFixture = bookmarkTestsFixture;
            _repositoryMock = _bookmarkTestsFixture.GetBookmarkRepositoryMock();
            _bookmarkService = new BookmarkService(_repositoryMock.Object);
        }

        [Fact(DisplayName = "Create bookmark with success, should return ResultResponse with success")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_ShouldReturnResultResponseWithSuccess()
        {
            var bookmark = _bookmarkTestsFixture.GetFakeBookmark();

            var resultBookmarkCreated = await _bookmarkService.CreateAsync(bookmark);

            resultBookmarkCreated.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.CreateAsync(bookmark), Times.Once);
        }

        [Fact(DisplayName = "Create an invalid bookmark, should return ResultResponse with error")]
        [Trait("Category", "Create")]
        public async Task CreateAsync_InvalidBookmark_ShouldReturnResultResponseWithError()
        {
            var invalidBookmark = _bookmarkTestsFixture.GetFakeInvalidBookmark();

            var resultBookmarkCreated = await _bookmarkService.CreateAsync(invalidBookmark);

            resultBookmarkCreated.Success.Should().BeFalse();
            _repositoryMock.Verify(rm => rm.CreateAsync(It.IsAny<Bookmark>()), Times.Never);
        }

        [Fact(DisplayName = "Delete bookmark with success, should return ResultResponse with success")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_ShouldReturnResultResponseWithSuccess()
        {
            var bookmarkId = _bookmarkTestsFixture.GetRandomInt();

            var resultBookmarkDeleted = await _bookmarkService.DeleteAsync(bookmarkId);

            resultBookmarkDeleted.Success.Should().BeTrue();
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<Bookmark>()), Times.Once);
        }

        [Fact(DisplayName = "Delete bookmark with inexistent id, should return ResultResponse with error")]
        [Trait("Category", "Delete")]
        public async Task DeleteAsync_InexistentId_ShouldReturnResultResponseWithError()
        {
            var bookmarkService = new BookmarkService(_repositoryMock.Object);

            var resultBookmarkDeleted = await _bookmarkService.DeleteAsync(_bookmarkTestsFixture.GetInexistentBoomarkId());

            resultBookmarkDeleted.Success.Should().BeFalse();
            resultBookmarkDeleted.ValidationErrors.FirstOrDefault().Message.Should().Be("The bookmark wasn't found");
            _repositoryMock.Verify(rm => rm.DeleteAsync(It.IsAny<Bookmark>()), Times.Never);
        }
    }
}

using CreatingAPI.Domain.Bookmarks.Interfaces;
using CreatingAPI.Domain.Core;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Bookmarks.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkRepository _bookmarkRepository;

        public BookmarkService(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        public async Task<ValidationResult> CreateAsync(Bookmark bookmark)
        {
            if (!bookmark.IsValid())
                return new ValidationResult(false, bookmark.ValidationErrors);

            var createdBookmarkId = await _bookmarkRepository.CreateAsync(bookmark);

            if (createdBookmarkId <= 0)
                return new ValidationResult(false, new ValidationError("There was an error while creating the bookmark"));

            return new ValidationResult(true);
        }

        public async Task<ValidationResult> DeleteAsync(int id)
        {
            if (id <= 0) return new ValidationResult(false, new ValidationError("The bookmark is invalid"));

            var bookmark = await _bookmarkRepository.GetAsync(id);

            if (bookmark == null)
                return new ValidationResult(false, new ValidationError("The bookmark wasn't found"));

            var bookmarkWasDeleted = await _bookmarkRepository.DeleteAsync(bookmark);

            if (!bookmarkWasDeleted)
                return new ValidationResult(false, new ValidationError("There was an error while deleting the bookmark"));

            return new ValidationResult(true);
        }
    }
}

using CreatingAPI.Domain.Core;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Bookmarks.Interfaces
{
    public interface IBookmarkService
    {
        Task<ValidationResult> CreateBookmark(Bookmark bookmark);
        Task<ValidationResult> DeleteBookmark(int id);
    }
}

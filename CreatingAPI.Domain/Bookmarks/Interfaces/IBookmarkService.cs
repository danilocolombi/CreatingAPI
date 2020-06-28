using CreatingAPI.Domain.Core;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Bookmarks.Interfaces
{
    public interface IBookmarkService
    {
        Task<ResultResponseBusiness> CreateBookmark(Bookmark bookmark);
        Task<ResultResponseBusiness> DeleteBookmark(int id);
    }
}

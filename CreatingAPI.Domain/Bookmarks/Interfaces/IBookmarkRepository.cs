using System.Threading.Tasks;

namespace CreatingAPI.Domain.Bookmarks.Interfaces
{
    public interface IBookmarkRepository
    {
        Task<int> CreateBookmark(Bookmark bookmark);
        Task<bool> DeleteBookmark(Bookmark bookmark);
        Task<Bookmark> GetBookmark(int id);
    }
}

using System.Threading.Tasks;

namespace CreatingAPI.Domain.Bookmarks.Interfaces
{
    public interface IBookmarkRepository
    {
        Task<int> CreateAsync(Bookmark bookmark);
        Task<bool> DeleteAsync(Bookmark bookmark);
        Task<Bookmark> GetAsync(int id);
    }
}

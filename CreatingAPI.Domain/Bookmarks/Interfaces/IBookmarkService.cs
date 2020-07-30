using CreatingAPI.Domain.Core;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Bookmarks.Interfaces
{
    public interface IBookmarkService
    {
        Task<ValidationResult> CreateAsync(Bookmark bookmark);
        Task<ValidationResult> DeleteAsync(int id);
    }
}

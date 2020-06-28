using CreatingAPI.Application.Bookmarks.ViewModels;
using CreatingAPI.Application.Core;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Bookmarks.Interfaces
{
    public interface IBookmarkAppService
    {
        Task<ResultResponse> CreateBookmark(BookmarkCreationViewModel bookmarkCreationViewModel);
        Task<ResultResponse> DeleteBookmark(int idBookmark);
    }
}

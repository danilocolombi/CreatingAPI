using CreatingAPI.Application.Bookmarks.ViewModels;
using CreatingAPI.Application.Core;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Bookmarks.Interfaces
{
    public interface IBookmarkAppService
    {
        Task<ResultResponse> CreateAsync(BookmarkCreationViewModel bookmarkCreationViewModel);
        Task<ResultResponse> DeleteAsync(int idBookmark);
    }
}

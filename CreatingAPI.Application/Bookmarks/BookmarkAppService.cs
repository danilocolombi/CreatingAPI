using AutoMapper;
using CreatingAPI.Application.Bookmarks.Interfaces;
using CreatingAPI.Application.Bookmarks.ViewModels;
using CreatingAPI.Application.Core;
using CreatingAPI.Domain.Bookmarks;
using CreatingAPI.Domain.Bookmarks.Interfaces;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Bookmarks
{
    public class BookmarkAppService : IBookmarkAppService
    {
        private readonly IBookmarkService _bookmarkService;
        private readonly IMapper _mapper;

        public BookmarkAppService(IBookmarkService bookmarkService, IMapper mapper)
        {
            _bookmarkService = bookmarkService;
            _mapper = mapper;
        }

        public async Task<ResultResponse> CreateAsync(BookmarkCreationViewModel bookmarkCreationViewModel)
        {
            var bookmark = _mapper.Map<Bookmark>(bookmarkCreationViewModel);

            var businessResult = await _bookmarkService.CreateAsync(bookmark);

            return new ResultResponse(businessResult, Operation.CREATE);
        }

        public async Task<ResultResponse> DeleteAsync(int idBookmark)
        {
            var businessResult = await _bookmarkService.DeleteAsync(idBookmark);

            return new ResultResponse(businessResult, Operation.DELETE);
        }
    }
}

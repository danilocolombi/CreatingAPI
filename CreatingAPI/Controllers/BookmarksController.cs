using CreatingAPI.Application.Bookmarks.Interfaces;
using CreatingAPI.Application.Bookmarks.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CreatingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarksController : ControllerBase
    {
        private readonly IBookmarkAppService _bookmarkAppService;

        public BookmarksController(IBookmarkAppService bookmarkAppService)
        {
            _bookmarkAppService = bookmarkAppService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] BookmarkCreationViewModel bookmark)
        {
            var resultBookmarkCreated = await _bookmarkAppService.CreateAsync(bookmark);

            if (resultBookmarkCreated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultBookmarkCreated.Errors);

            return CreatedAtRoute("", bookmark);
        }

        [HttpDelete("{bookmarkId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int bookmarkId)
        {
            var resultBookmarkDeleted = await _bookmarkAppService.DeleteAsync(bookmarkId);

            if (resultBookmarkDeleted.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultBookmarkDeleted.Errors);

            return NoContent();
        }
    }
}

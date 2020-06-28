using CreatingAPI.Application.Unscrumbles.Interfaces;
using CreatingAPI.Application.Unscrumbles.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CreatingAPI.Controllers
{
    [Produces("application/json", "application/xml")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UnscrumblesController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUnscrumble([FromBody] UnscrumbleCreationViewModel usncrumble, [FromServices] IUnscrumbleAppService unscrumbleAppService)
        {
            var resultUnscrumbleCreated = await unscrumbleAppService.CreateUnscrumble(usncrumble);

            if (resultUnscrumbleCreated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultUnscrumbleCreated.Errors);

            return CreatedAtRoute("", usncrumble);
        }

        [HttpPut("{unscrumbleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUnscrumble(int unscrumbleId,[FromBody] UnscrumbleCreationViewModel usncrumble,
                                                                    [FromServices] IUnscrumbleAppService unscrumbleAppService)
        {
            var resultUnscrumbleUpdated = await unscrumbleAppService.UpdateUnscrumble(unscrumbleId, usncrumble);

            if (resultUnscrumbleUpdated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultUnscrumbleUpdated.Errors);

            if (resultUnscrumbleUpdated.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultUnscrumbleUpdated.Errors);

            return Ok();
        }

        [HttpDelete("{unscrumbleId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUnscrumble(int unscrumbleId, [FromServices] IUnscrumbleAppService unscrumbleAppService)
        {
            var resultUnscrumbleDeleted = await unscrumbleAppService.DeleteUnscrumble(unscrumbleId);

            if (resultUnscrumbleDeleted.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultUnscrumbleDeleted.Errors);

            return NoContent();
        }

        [HttpGet("{unscrumbleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUnscrumble(int unscrumbleId, [FromServices] IUnscrumbleAppService unscrumbleAppService)
        {
            var unscrumble = await unscrumbleAppService.GetUnscrumble(unscrumbleId);

            if (unscrumble == null) return NotFound();

            return Ok(unscrumble);
        }
    }
}

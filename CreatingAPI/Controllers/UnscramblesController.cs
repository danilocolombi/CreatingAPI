using CreatingAPI.Application.Unscrambles.Interfaces;
using CreatingAPI.Application.Unscrambles.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CreatingAPI.Controllers
{
    [Produces("application/json", "application/xml")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UnscramblesController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] UnscrambleCreationViewModel usncramble, [FromServices] IUnscrambleAppService unscrambleAppService)
        {
            var resultUnscrambleCreated = await unscrambleAppService.CreateAsync(usncramble);

            if (resultUnscrambleCreated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultUnscrambleCreated.Errors);

            return CreatedAtRoute("", usncramble);
        }

        [HttpPut("{unscrambleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int unscrambleId, [FromBody] UnscrambleCreationViewModel usncramble,
                                                                    [FromServices] IUnscrambleAppService unscrambleAppService)
        {
            var resultUnscrambleUpdated = await unscrambleAppService.UpdateAsync(unscrambleId, usncramble);

            if (resultUnscrambleUpdated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultUnscrambleUpdated.Errors);

            if (resultUnscrambleUpdated.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultUnscrambleUpdated.Errors);

            return Ok();
        }

        [HttpDelete("{unscrambleId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int unscrambleId, [FromServices] IUnscrambleAppService unscrambleAppService)
        {
            var resultUnscrambleDeleted = await unscrambleAppService.DeleteAsync(unscrambleId);

            if (resultUnscrambleDeleted.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultUnscrambleDeleted.Errors);

            return NoContent();
        }

        [HttpGet("{unscrambleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int unscrambleId, [FromServices] IUnscrambleAppService unscrambleAppService)
        {
            var unscramble = await unscrambleAppService.GetAsync(unscrambleId);

            if (unscramble == null) return NotFound();

            return Ok(unscramble);
        }

        [HttpGet("ShuffledExercises/{unscrambleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUnscrumble(int unscrambleId, [FromQuery] bool randomizeOrder, [FromServices] IUnscrambleAppService unscrambleAppService)
        {
            var exercises = await unscrambleAppService.GetShuffledExercises(unscrambleId, randomizeOrder);

            if (exercises == null) return NotFound();

            return Ok(exercises);
        }
    }
}

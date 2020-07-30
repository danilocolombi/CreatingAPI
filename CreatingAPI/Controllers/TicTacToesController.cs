using CreatingAPI.Application.TicTacToes.Interfaces;
using CreatingAPI.Application.TicTacToes.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CreatingAPI.Controllers
{
    [Produces("application/json", "application/xml")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TicTacToesController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] TicTacToeCreationViewModel ticTacToeViewModel, [FromServices] ITicTacToeAppService ticTacToeAppService)
        {
            var resultTicTacToeCreated = await ticTacToeAppService.CreateAsync(ticTacToeViewModel);

            if (resultTicTacToeCreated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultTicTacToeCreated.Errors);

            return CreatedAtRoute("", ticTacToeViewModel);
        }

        [HttpPut("{ticTacToeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int ticTacToeId, [FromBody] TicTacToeCreationViewModel ticTacToeViewModel,
                                                                    [FromServices] ITicTacToeAppService ticTacToeAppService)
        {
            var resultTicTacToeUpdated = await ticTacToeAppService.UpdateAsync(ticTacToeId, ticTacToeViewModel);

            if (resultTicTacToeUpdated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultTicTacToeUpdated.Errors);

            if (resultTicTacToeUpdated.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultTicTacToeUpdated.Errors);

            return Ok();
        }

        [HttpDelete("{ticTacToeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int ticTacToeId, [FromServices] ITicTacToeAppService ticTacToeAppService)
        {
            var resultTicTacToeDeleted = await ticTacToeAppService.DeleteAsync(ticTacToeId);

            if (resultTicTacToeDeleted.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultTicTacToeDeleted.Errors);

            return NoContent();
        }

        [HttpGet("{ticTacToeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int ticTacToeId, [FromServices] ITicTacToeAppService ticTacToeAppService)
        {
            var ticTacToe = await ticTacToeAppService.GetAsync(ticTacToeId);

            if (ticTacToe == null) return NotFound();

            return Ok(ticTacToe);
        }
    }
}

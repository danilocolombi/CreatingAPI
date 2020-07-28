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
        public async Task<IActionResult> CreateTicTacToe([FromBody] TicTacToeCreationViewModel ticTacToeViewModel, [FromServices] ITicTacToeAppService ticTacToeAppService)
        {
            var resultTicTacToeCreated = await ticTacToeAppService.CreateTicTacToe(ticTacToeViewModel);

            if (resultTicTacToeCreated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultTicTacToeCreated.Errors);

            return CreatedAtRoute("", ticTacToeViewModel);
        }

        [HttpPut("{ticTacToeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTicTacToe(int ticTacToeId, [FromBody] TicTacToeCreationViewModel ticTacToeViewModel,
                                                                    [FromServices] ITicTacToeAppService ticTacToeAppService)
        {
            var resultTicTacToeUpdated = await ticTacToeAppService.UpdateTicTacToe(ticTacToeId, ticTacToeViewModel);

            if (resultTicTacToeUpdated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultTicTacToeUpdated.Errors);

            if (resultTicTacToeUpdated.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultTicTacToeUpdated.Errors);

            return Ok();
        }

        [HttpDelete("{ticTacToeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTicTacToe(int ticTacToeId, [FromServices] ITicTacToeAppService ticTacToeAppService)
        {
            var resultTicTacToeDeleted = await ticTacToeAppService.DeleteTicTacToe(ticTacToeId);

            if (resultTicTacToeDeleted.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultTicTacToeDeleted.Errors);

            return NoContent();
        }

        [HttpGet("{ticTacToeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUnscrumble(int ticTacToeId, [FromServices] ITicTacToeAppService ticTacToeAppService)
        {
            var ticTacToe = await ticTacToeAppService.GetTicTacToe(ticTacToeId);

            if (ticTacToe == null) return NotFound();

            return Ok(ticTacToe);
        }
    }
}

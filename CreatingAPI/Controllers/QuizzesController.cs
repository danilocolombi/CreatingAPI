using CreatingAPI.Application.Quizzes.Interfaces;
using CreatingAPI.Application.Quizzes.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CreatingAPI.Controllers
{
    [Produces("application/json", "application/xml")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    public class QuizzesController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] QuizCreationViewModel quizViewModel, [FromServices] IQuizAppService quizAppService)
        {
            var resultQuizCreated = await quizAppService.CreateAsync(quizViewModel);

            if (resultQuizCreated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultQuizCreated.Errors);

            return CreatedAtRoute("", quizViewModel);
        }

        [HttpPut("{quizId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int quizId, [FromBody] QuizCreationViewModel quizViewModel, [FromServices] IQuizAppService quizAppService)
        {
            var resultUpdatedQuiz = await quizAppService.UpdateAsync(quizId, quizViewModel);

            if (resultUpdatedQuiz.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultUpdatedQuiz.Errors);

            if (resultUpdatedQuiz.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultUpdatedQuiz.Errors);

            return Ok();
        }

        [HttpDelete("{quizId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int quizId, [FromServices] IQuizAppService quizAppService)
        {
            var resultQuizDeleted = await quizAppService.DeleteAsync(quizId);

            if (resultQuizDeleted.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultQuizDeleted.Errors);

            return NoContent();
        }

        [HttpGet("{quizId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int quizId, [FromServices] IQuizAppService quizAppService)
        {
            var quiz = await quizAppService.GetAsync(quizId);

            if (quiz == null) return NotFound();

            return Ok(quiz);
        }
    }
}

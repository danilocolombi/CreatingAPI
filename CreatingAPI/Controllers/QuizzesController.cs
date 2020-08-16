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
        private readonly IQuizAppService _quizAppService;

        public QuizzesController(IQuizAppService quizAppService)
        {
            _quizAppService = quizAppService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] QuizCreationViewModel quizViewModel)
        {
            var resultQuizCreated = await _quizAppService.CreateAsync(quizViewModel);

            if (resultQuizCreated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultQuizCreated.Errors);

            return CreatedAtRoute("", quizViewModel);
        }

        [HttpPut("{quizId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int quizId, [FromBody] QuizCreationViewModel quizViewModel)
        {
            var resultUpdatedQuiz = await _quizAppService.UpdateAsync(quizId, quizViewModel);

            if (resultUpdatedQuiz.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultUpdatedQuiz.Errors);

            if (resultUpdatedQuiz.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultUpdatedQuiz.Errors);

            return Ok();
        }

        [HttpDelete("{quizId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int quizId)
        {
            var resultQuizDeleted = await _quizAppService.DeleteAsync(quizId);

            if (resultQuizDeleted.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultQuizDeleted.Errors);

            return NoContent();
        }

        [HttpGet("{quizId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int quizId)
        {
            var quiz = await _quizAppService.GetAsync(quizId);

            if (quiz == null) return NotFound();

            return Ok(quiz);
        }
    }
}

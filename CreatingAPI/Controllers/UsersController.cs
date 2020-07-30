using CreatingAPI.Application.Users.Interfaces;
using CreatingAPI.Application.Users.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CreatingAPI.Controllers
{
    [Produces("application/json", "application/xml")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] UserCreationViewModel user, [FromServices] IUserAppService userAppService)
        {
            var resultUserCreated = await userAppService.CreateAsync(user);

            if (resultUserCreated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultUserCreated.Errors);

            return CreatedAtRoute("", user);
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePasswordAsync(int userId, string newPassword, [FromServices] IUserAppService userAppService)
        {
            var resultUserUpdated = await userAppService.ChangePasswordAsync(userId, newPassword);

            if (resultUserUpdated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultUserUpdated.Errors);

            if (resultUserUpdated.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultUserUpdated.Errors);

            return Ok();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int userId, [FromServices] IUserAppService userAppService)
        {
            var resultUserDeleted = await userAppService.DeleteAsync(userId);

            if (resultUserDeleted.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultUserDeleted.Errors);

            return NoContent();
        }
    }
}

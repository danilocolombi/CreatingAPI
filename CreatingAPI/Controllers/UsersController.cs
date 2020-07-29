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
        public async Task<IActionResult> CreateUser([FromBody] UserCreationViewModel user, [FromServices] IUserAppService userAppService)
        {
            var resultUserCreated = await userAppService.CreateUser(user);

            if (resultUserCreated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultUserCreated.Errors);

            return CreatedAtRoute("", user);
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(int userId, string newPassword, [FromServices] IUserAppService userAppService)
        {
            var resultUserUpdated = await userAppService.ChangePassword(userId, newPassword);

            if (resultUserUpdated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultUserUpdated.Errors);

            if (resultUserUpdated.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultUserUpdated.Errors);

            return Ok();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int userId, [FromServices] IUserAppService userAppService)
        {
            var resultUserDeleted = await userAppService.DeleteUser(userId);

            if (resultUserDeleted.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultUserDeleted.Errors);

            return NoContent();
        }
    }
}

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
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] UserCreationViewModel user)
        {
            var resultUserCreated = await _userAppService.CreateAsync(user);

            if (resultUserCreated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultUserCreated.Errors);

            return CreatedAtRoute("", user);
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePasswordAsync(int userId, string newPassword)
        {
            var resultUserUpdated = await _userAppService.ChangePasswordAsync(userId, newPassword);

            if (resultUserUpdated.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultUserUpdated.Errors);

            if (resultUserUpdated.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultUserUpdated.Errors);

            return Ok();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int userId)
        {
            var resultUserDeleted = await _userAppService.DeleteAsync(userId);

            if (resultUserDeleted.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultUserDeleted.Errors);

            return NoContent();
        }


        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int userId)
        {
            var activities = await _userAppService.GetUserActivitiesAsync(userId);

            if (activities == null)
                return NotFound();

            return Ok(activities);
        }
    }
}

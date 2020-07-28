using CreatingAPI.Application.Pickers.Interfaces;
using CreatingAPI.Application.Pickers.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CreatingAPI.Controllers
{
    [Produces("application/json", "application/xml")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    public class PickersController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePicker([FromBody] PickerCreationViewModel pickerViewModel, [FromServices] IPickerAppService pickerAppService)
        {
            var resultCreatedPicker = await pickerAppService.CreatePicker(pickerViewModel);

            if (resultCreatedPicker.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultCreatedPicker.Errors);

            return CreatedAtRoute("", pickerViewModel);
        }

        [HttpPut("{pickerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePicker(int pickerId, [FromBody] PickerCreationViewModel pickerViewModel, [FromServices] IPickerAppService pickerAppService)
        {
            var resultUpdatedPicker = await pickerAppService.UpdatePicker(pickerId, pickerViewModel);

            if (resultUpdatedPicker.StatusCode == Application.Core.StatusCode.BAD_REQUEST)
                return BadRequest(resultUpdatedPicker.Errors);

            if (resultUpdatedPicker.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultUpdatedPicker.Errors);

            return Ok();
        }

        [HttpDelete("{pickerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePicker(int pickerId, [FromServices] IPickerAppService pickerAppService)
        {
            var resultDeletedPicker = await pickerAppService.DeletePicker(pickerId);

            if (resultDeletedPicker.StatusCode == Application.Core.StatusCode.NOT_FOUND)
                return NotFound(resultDeletedPicker.Errors);

            return NoContent();
        }

        [HttpGet("{pickerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPicker(int pickerId, [FromServices] IPickerAppService pickerAppService)
        {
            var picker = await pickerAppService.GetPicker(pickerId);

            if (picker == null) return NotFound();

            return Ok(picker);
        }
    }
}

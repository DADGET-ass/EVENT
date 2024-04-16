using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Practice2.Controllers
{
    [ApiController]

    public class CalendarController : Controller
    {
        private ICalendarsService _calendarsService;

        public CalendarController(ICalendarsService calendarsService)
        {
            _calendarsService = calendarsService;
        }

        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> Create()
        {
            var result = await _calendarsService.Create();

            return Ok(new { result = result });
        }

        [HttpDelete("[controller]/[action]")]
        public async Task<IActionResult> Delete([FromHeader][Required] int CalendarId)
        {
            try
            {
                var result = await _calendarsService.Delete(CalendarId);

                return Ok(new { result = result });
            }
            catch (NotCalendarException ex)
            {
                return StatusCode(404, new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
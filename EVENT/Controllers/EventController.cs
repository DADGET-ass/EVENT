using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Practice2.Controllers
{
    [ApiController]

    public class EventController : Controller
    {
        private IEventsService _eventsService;

        public EventController(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> Create([FromBody] CreateEventModel model)
        {
            try
            {
                var result = await _eventsService.CreateEvent(model);

                return Ok(new { result = result });
            }  catch (NotCalendarException ex)
            {
                return StatusCode(404, new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("[controller]/[action]")]
        public async Task<IActionResult> GetAll([FromHeader] int CalendarId)
        {
            try
            {
                var result = await _eventsService.GetAll(CalendarId);

                return Ok(new { events = result });
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

        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> Join([FromBody] EventsInCalendarModel model)
        {
            try
            {
                var result = await _eventsService.EventsInCalendar(model);

                return Ok(new { result = result });
            } catch (NotCalendarException ex)
            {
                return StatusCode(404, new { error = ex.Message });
            } catch (NotEventException ex)
            {
                return StatusCode(404, new { error = ex.Message });
            } catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("[controller]/[action]")]
        public async Task<IActionResult> Refresh([FromBody]RefreshEventModel model)
        {
            try
            {
                var result = await _eventsService.RefreshEvent(model);

                return Ok(new { result = result });
            }
            catch (NotEventException ex)
            {
                return StatusCode(404, new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("[controller]/[action]")]
        public async Task<IActionResult> Delete([FromHeader][Required] int EventId)
        {
            try
            {
                var result = await _eventsService.DeleteEvent(EventId);

                return Ok(new { result = result });
            }
            catch (NotEventException ex)
            {
                return StatusCode(404, new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("[controller]/[action]")]
        public async Task<IActionResult> DeleteAllEvents([FromHeader][Required] int CalendarId)
        {
            try
            {
                var result = await _eventsService.DeleteAllEvents(CalendarId);

                return Ok(new { result = result });
            }
            catch (NotEventException ex)
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
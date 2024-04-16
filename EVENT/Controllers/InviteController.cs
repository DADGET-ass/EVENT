using Microsoft.AspNetCore.Mvc;

namespace Practice2.Controllers
{
    [ApiController]

    public class InviteController : Controller
    {
        private IInviteService _inviteService;

        public InviteController(IInviteService inviteService)
        {
            _inviteService = inviteService;
        }

        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> Create([FromBody] CreateInviteModel model)
        {
            try
            {
                var result = await _inviteService.CreateInvite(model);

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
        public async Task<IActionResult> Delete([FromHeader] int InviteId)
        {
            try
            {
                var result = await _inviteService.DeleteInvite(InviteId);

                return Ok(new { result = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("[controller]/[action]")]
        public async Task<IActionResult> DeleteAll([FromHeader] int EventId)
        {
            try
            {
                var result = await _inviteService.DeleteInvitesByEvent(EventId);

                return Ok(new { result = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> Invite([FromBody] InviteModel model)
        {
            try
            {
                var result = await _inviteService.Invite(model);

                return Ok(new { result = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("[controller]/[action]")]
        public async Task<IActionResult> Edit([FromBody] InviteEditModel model)
        {
            try
            {
                var result = await _inviteService.Edit(model);

                return Ok(new { result = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
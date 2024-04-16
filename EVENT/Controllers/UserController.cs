using Microsoft.AspNetCore.Mvc;

namespace Practice2.Controllers
{
    [ApiController]

    public class UsersController : Controller
    {
        private IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> Registration(UserRegistrationModel model)
        {
            try
            {
                var result = await _usersService.Registration(model);
                return Ok(new { token = result });
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { error = ex.Message });
            }
        }

        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> Authorization(UserAuthorizationModel model)
        {
            try
            {
                var result = await _usersService.Authorization(model);
                return Ok(new { token = result });
            } 
            catch (NotCalendarException ex)
            {
                return StatusCode(404, new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { error = ex.Message });
            }
        }
    }
}

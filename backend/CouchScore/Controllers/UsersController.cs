using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CouchScore.Models;
using CouchScore.Services;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate/login")]
        public async Task<IActionResult> AuthenticateLogin([FromBody]AuthenticateRequest account)
        {
            var response = await _userService.AuthenticateLoginAsync(account);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("authenticate/register")]
        public async Task<IActionResult> AuthenticateRegister([FromBody]AuthenticateRequestRegister account)
        {
            var response = await _userService.AuthenticateRegisterAsync(account);

            if (response == null)
                return BadRequest(new { message = "Account information is incorrect" });

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
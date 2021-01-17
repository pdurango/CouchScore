using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CouchScore.Models;
using CouchScore.Services;

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
        public IActionResult AuthenticateLogin([FromBody]AuthenticateRequestLogin account)
        {
            var response = _userService.AuthenticateLogin(account);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("authenticate/register")]
        public IActionResult AuthenticateRegister([FromBody]AuthenticateRequestRegister account)
        {
            var response = _userService.AuthenticateRegister(account);

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
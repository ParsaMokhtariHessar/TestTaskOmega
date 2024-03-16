using Microsoft.AspNetCore.Mvc;
using TestTaskOmega.Identity.IdentityServices.AuthenticationService;

namespace TestTaskOmega.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] string username, [FromBody] string password)
        {
            try
            {
                var token = await _authenticationService.Login(username, password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //This has to be Authorized and only by admin
        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateUser([FromBody] string email,
                                                           [FromBody] string username,
                                                           [FromBody] string password)
        {
            try
            {
                var userId = await _authenticationService.CreateUser(email, username, password);
                return Ok(new { UserId = userId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

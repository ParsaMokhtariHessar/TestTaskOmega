using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTaskOmega.API.Dtos;
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
        public async Task<ActionResult<string>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _authenticationService.Login(loginDto.Username, loginDto.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<string>> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                var userId = await _authenticationService.CreateUser(createUserDto.Email, createUserDto.Username, createUserDto.Password);
                return Ok(new { UserId = userId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

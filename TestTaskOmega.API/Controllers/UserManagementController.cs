using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTaskOmega.Identity.IdentityModels;
using TestTaskOmega.Identity.IdentityServices.UserManagmentService;

namespace TestTaskOmega.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ManagerOnly")]
    public class UserController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;

        public UserController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<UserRole>> GetUser(string username)
        {
            try
            {
                var userRole = await _userManagementService.GetUser(username);
                return Ok(userRole);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<UserRole>>> GetUsers()
        {
            try
            {
                var userRoles = await _userManagementService.GetUsers();
                return Ok(userRoles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


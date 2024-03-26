using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TestTaskOmega.Identity.IdentityModels;

namespace TestTaskOmega.Identity.IdentityServices.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> GetUserFromClaimsAsync()
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext?.User;

            if (claimsPrincipal?.Identity?.IsAuthenticated == true)
            {
                var userNameClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userNameClaim != null)
                {
                    // Retrieve the user from UserManager using the user ID
                    return await _userManager.FindByNameAsync(userNameClaim);
                }
            }

            return null;
        }
    }
}

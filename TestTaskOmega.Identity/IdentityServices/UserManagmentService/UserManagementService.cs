using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestTaskOmega.Identity.IdentityModels;

namespace TestTaskOmega.Identity.IdentityServices.UserManagmentService
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserManagementService(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var userIdString = _contextAccessor.HttpContext?.User?.FindFirstValue("Id"); //this is how you get Id instead of name! 

                if (Guid.TryParse(userIdString, out Guid userId))
                {
                    return userId;
                }
                else
                {
                    // Handle the case when parsing fails, for example, return a default Guid.
                    return Guid.Empty; // or throw an exception, or any other appropriate handling
                }
            }
        }

        public async Task<UserRole> GetUser(string username)
        {
            var userRole = await _userManager.FindByNameAsync(username);
            if (userRole == null)
            {
                throw new Exception($"User with {username} not found.");
            }

            return new UserRole
            {
                Id = Guid.Parse(userRole.Id),
                UserName = userRole.UserName ?? string.Empty,
            };
        }

        public async Task<List<UserRole>> GetUsers()
        {
            var userRoles = await _userManager.GetUsersInRoleAsync("User");
            return userRoles.Select(q => new UserRole
            {
                Id = Guid.Parse(q.Id),
                UserName = q.UserName ?? string.Empty
            }).ToList();
        }
    }
}


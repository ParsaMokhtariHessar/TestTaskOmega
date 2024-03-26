using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskOmega.Identity.IdentityModels;

namespace TestTaskOmega.Identity.IdentityServices.UserService
{
    public interface IUserService
    {
        public Task<ApplicationUser?> GetUserFromClaimsAsync();
    }
}

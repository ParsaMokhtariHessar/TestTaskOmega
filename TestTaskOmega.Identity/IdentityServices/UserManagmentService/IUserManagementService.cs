using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskOmega.Identity.IdentityModels;

namespace TestTaskOmega.Identity.IdentityServices.UserManagmentService
{
    public interface IUserManagementService
    {
        Task<List<UserRole>> GetUsers();
        Task<UserRole> GetUser(string username);
        public Guid UserId { get; }
    }
}

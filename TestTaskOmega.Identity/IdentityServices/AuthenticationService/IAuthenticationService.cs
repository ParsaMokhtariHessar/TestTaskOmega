using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskOmega.Identity.IdentityServices.AuthenticationService
{
    public interface IAuthenticationService
    {
        // This is the auth service
        Task<string> Login(string username, string password);
        Task<string> CreateUser(string email, string username, string password);
    }
}

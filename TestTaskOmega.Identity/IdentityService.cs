using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TestTaskOmega.Application;
using TestTaskOmega.Application.Contracts;

namespace TestTaskOmega.Identity
{
    internal class IdentityService : IIdentityService

    {
        public string LogIn(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public int Register(string Email, string Password)
        {
            throw new NotImplementedException();
        }
    }
}

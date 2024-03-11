using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskOmega.Application.Contracts
{
    public interface IIdentityService
    {
        int Register(string Email, string Password);
        string LogIn(string Email, string Password);
    }
}

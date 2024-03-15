using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskOmega.Application.Exeptions
{
    public class EntityNotUniqueException : Exception
    {
        public EntityNotUniqueException(string message) : base(message)
        {
        }
    }
}


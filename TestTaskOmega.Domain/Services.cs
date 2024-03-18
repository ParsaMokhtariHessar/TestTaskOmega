using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskOmega.Domain
{
    public class Services : BaseEntity<string>
    {
        public string ServiceName
        {
            get => Value;
            set => Value = value;
        }

        // Constructor to pass values back to the base class
        public Services(int createdBy, string value) : base(createdBy, value)
        {
        }

        // Constructor to pass values back to the base class
        public Services(int createdBy) : base(createdBy)
        {
        }

        // Constructor with zero arguments
        public Services() : base()
        {
        }
    }
}

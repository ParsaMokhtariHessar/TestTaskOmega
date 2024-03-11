using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskOmega.Domain
{
    public class ServicesHistory
    {
        public int Id { get; set; }
        public int OriginalEntityId { get; set; }
        public string ServiceName { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        // Include other properties to store the historical state
    }
}

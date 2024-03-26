using TestTaskOmega.Identity.IdentityModels;

namespace TestTaskOmega.Domain
{
    public class Services : BaseEntity<string>
    {
        public string ServiceName
        {
            get => Value;
        }

        public Services() : base()
        {
            
        }
        // Constructor to pass values back to the base class
        public Services(string value, ApplicationUser createdBy) : base(value, createdBy)
        {
        }
    }
}

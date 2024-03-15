namespace TestTaskOmega.Domain
{
    public class BaseEntityHistory
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int ModifiedBy { get; set; }
        public int CreatedBy { get; set; }
        public int DeletedBy { get; set; }
    }
}

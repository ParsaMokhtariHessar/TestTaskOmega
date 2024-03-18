namespace TestTaskOmega.Domain.Utilities
{
    public class Modifications<T>
    {
        public int Id;
        public DateTime ModifiedAt { get; set; }
        public int ModifiedBy { get; set; }
        public T? ModifiedTo { get; set; }
    }
}

using TestTaskOmega.Domain.Utilities;

namespace TestTaskOmega.Domain
{
    public abstract class BaseEntity<T>
    {
        private List<Modifications<T>> _modifications = new List<Modifications<T>>();

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        private int _createdBy = default;
        public int CreatedBy
        {
            get => _createdBy;
            set
            {
                _createdBy = value;
                // If Value is already initialized, perform the creation modification
                if (_value != null)
                {
                    Modify(_value, modifiedBy: CreatedBy);
                }
            }
        }

        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                if (!EqualityComparer<T>.Default.Equals(_value, value) && !EqualityComparer<T>.Default.Equals(value, default(T)))
                {
                    Modify(value, modifiedBy: CreatedBy);
                }
                _value = value;
            }
        }

        public IEnumerable<Modifications<T>> Modifications => _modifications;
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public bool IsDeleted => DeletedAt.HasValue;
        public bool IsModified => _modifications.Count > 0;

        public void Modify(T newValue, int modifiedBy)
        {
            if (!EqualityComparer<T>.Default.Equals(Value, newValue))
            {
                _modifications.Add(new Modifications<T>
                {
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = modifiedBy,
                    ModifiedTo = newValue
                });
                Value = newValue;
            }
        }

        public void Delete(int deletedBy)
        {
            if (!IsDeleted)
            {
                Modify(default!, modifiedBy: deletedBy);
                DeletedAt = DateTime.Now;
                DeletedBy = deletedBy;
            }
        }

        // Constructor to initialize creation modification with createdBy and value
        protected BaseEntity(int createdBy, T value) : this(createdBy)
        {
            Value = value;
        }

        // Constructor to initialize creation modification with createdBy
        protected BaseEntity(int createdBy)
        {
            CreatedBy = createdBy;
            _value = default!;
            Modify(_value, modifiedBy: CreatedBy); // Assuming 0 as the default value for createdBy during creation modification
        }

        protected BaseEntity() : this(default, default) // Assuming default values for CreatedBy and Value
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace TestTaskOmega.Domain
{
    public abstract class BaseEntity<T>
    {
        [Key]
        public int Id { get; set; }

        private DateTime _createdAt;
        public DateTime CreatedAt => _createdAt;

        private int _createdBy;
        public int CreatedBy => _createdBy;

        private T _value = default!;
        public T Value => _value;

        private readonly List<EntityModification<T>> _modifications = new List<EntityModification<T>>();
        public IEnumerable<EntityModification<T>> History => _modifications;

        private DateTime? _deletedAt;
        public DateTime? DeletedAt => _deletedAt;

        private int? _deletedBy;
        public int? DeletedBy => _deletedBy;

        public bool IsDeleted => DeletedAt.HasValue;

        public bool IsModified => _modifications.Count > 0;

        protected BaseEntity()
        {
            
        }
        protected BaseEntity(T value, int createdBy)
        {
            Create(value, createdBy);
        }

        public void Create(T value, int createdBy)
        {
            _value = value;
            _createdBy = createdBy;
            _createdAt = DateTime.Now;
            AddModification(value, createdBy);
        }

        public void Modify(T newValue, int modifiedBy)
        {
            AddModification(newValue, modifiedBy);
            _value = newValue;
        }

        public void Delete(int deletedBy)
        {
            _deletedAt = DateTime.Now;
            _deletedBy = deletedBy;
        }

        private void AddModification(T value, int modifiedBy)
        {
            _modifications.Add(new EntityModification<T>(value, modifiedBy));
        }
    }
}

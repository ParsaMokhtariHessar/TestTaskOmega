using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestTaskOmega.Domain;

public class EntityModification<T>
{
    private readonly DateTime _modifiedAt;
    private readonly int _modifiedBy;
    private readonly T? _modifiedTo;

    [Key]
    public int Id { get; set; }

    public DateTime ModifiedAt => _modifiedAt;
    public int ModifiedBy => _modifiedBy;
    public T? ModifiedTo => _modifiedTo;

    public EntityModification()
    {
    }

    public EntityModification(T modifiedValue, int modifierId)
    {
        _modifiedAt = DateTime.Now;
        _modifiedBy = modifierId;
        _modifiedTo = modifiedValue;
    }
}


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestTaskOmega.Domain;
using TestTaskOmega.Identity.IdentityModels;

public class EntityModification<T>
{
    private readonly DateTime _modifiedAt;
    private readonly ApplicationUser _modifiedBy = new();
    private readonly T? _modifiedTo;

    [Key]
    public int Id { get; set; }

    public DateTime ModifiedAt => _modifiedAt;
    public ApplicationUser ModifiedBy => _modifiedBy;
    public T? ModifiedTo => _modifiedTo;

    public EntityModification()
    {
    }

    public EntityModification(T modifiedValue, ApplicationUser modifier)
    {
        _modifiedAt = DateTime.Now;
        _modifiedBy = modifier;
        _modifiedTo = modifiedValue;
    }
}


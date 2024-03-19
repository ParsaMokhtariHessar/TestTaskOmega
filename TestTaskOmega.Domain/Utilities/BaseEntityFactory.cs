namespace TestTaskOmega.Domain.Utilities;

// Factory class for BaseEntity<T>
public static class BaseEntityFactory<T>
{
    // Factory method to create instances of BaseEntity<T>
    public static BaseEntity<T>? CreateEntity(T value, int createdBy)
    {
        return Activator.CreateInstance(typeof(BaseEntity<T>), value, createdBy) as BaseEntity<T>;
    }
}

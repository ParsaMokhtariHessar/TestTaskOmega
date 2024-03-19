using TestTaskOmega.Domain;

namespace TestTaskOmega.Application.RepositoryPattern
{
    public interface IRepository<TEntity, T> where TEntity : BaseEntity<T>
    {
        Task<IEnumerable<TEntity>> GetAllDeletedAsync();
        Task CreateAsync(T value);
        Task DeleteAsync(int entityId);
        Task<IEnumerable<EntityModification<T>>> GetHistory(int EntityId);
        Task<TEntity> GetByValue(T value);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByCreationDateAsync(DateTime creationDate);
        Task<TEntity> GetByIdAsync(int id);
        Task UpdateAsync(int entityId, T value);
    }
}
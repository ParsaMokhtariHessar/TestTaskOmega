using TestTaskOmega.Domain;

namespace TestTaskOmega.Application.RepositoryPattern
{
    public interface IRepository<TEntity, TEntityHistory>
        where TEntity : BaseEntity
        where TEntityHistory : BaseEntityHistory
    {
        void Create(TEntity entity, TEntityHistory entityHistory);
        void Delete(TEntity entity, TEntityHistory entityHistory);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntityHistory> GetAllHistoryByIdSortedByLatest(int id);
        TEntity GetByCreationDate(DateTime creationDate);
        TEntity GetById(int id);
        void Update(TEntity entity, TEntityHistory entityHistory);
    }
}
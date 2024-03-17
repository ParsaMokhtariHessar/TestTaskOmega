using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskOmega.Domain;

namespace TestTaskOmega.Application.RepositoryPattern
{
    public interface IRepository<TEntity, TEntityHistory>
        where TEntity : BaseEntity
        where TEntityHistory : BaseEntityHistory
    {
        Task<IEnumerable<TEntity>> GetAllDeletedAsync();
        Task CreateAsync(TEntity entity, TEntityHistory entityHistory);
        Task DeleteAsync(TEntity entity, TEntityHistory entityHistory);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntityHistory>> GetAllHistoryByIdSortedByLatestAsync(int id);
        Task<TEntity> GetByCreationDateAsync(DateTime creationDate);
        Task<TEntity> GetByIdAsync(int id);
        Task UpdateAsync(TEntity entity, TEntityHistory entityHistory);
    }
}
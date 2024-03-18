using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TestTaskOmega.Domain;
using TestTaskOmega.Domain.Utilities;

namespace TestTaskOmega.Application.RepositoryPattern
{
    public interface IRepository<TEntity, T> where TEntity : BaseEntity<T>
    {
        Task<IEnumerable<TEntity>> GetAllDeletedAsync();
        Task CreateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<IEnumerable<Modifications<T>>> GetHistory(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByCreationDateAsync(DateTime creationDate);
        Task<TEntity> GetByIdAsync(int id);
        Task UpdateAsync(TEntity entity);
    }
}
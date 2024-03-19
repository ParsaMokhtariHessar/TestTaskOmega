using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TestTaskOmega.Application.Exeptions;
using TestTaskOmega.DataAccess;
using TestTaskOmega.Domain;
using TestTaskOmega.Domain.Utilities;

namespace TestTaskOmega.Application.RepositoryPattern
{



    public class Repository<TEntity, T> : IRepository<TEntity, T> where TEntity : BaseEntity<T>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Repository(ApplicationDbContext dbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllDeletedAsync()
        {
            return await _dbContext.Set<TEntity>().Where(e => e.IsDeleted).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found.");
            }

            return entity;
        }

        public async Task<TEntity> GetByCreationDateAsync(DateTime creationDate)
        {
            var entity = await _dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.CreatedAt.Date == creationDate.Date);

            if (entity == null)
            {
                throw new NotFoundException($"Entity with creationDate {creationDate} not found.");
            }

            return entity;
        }

        public async Task<TEntity> GetByValue(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var entity = await _dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Value!.Equals(value)); //will it be null?

            if (entity == null)
            {
                throw new NotFoundException($"Entity with Value {value} not found.");
            }

            return entity;
        }

        public async Task<IEnumerable<EntityModification<T>>> GetHistory(int entityId)
        {
            var entity = await GetByIdAsync(entityId);
            return entity.History;
                
        }

        public async Task CreateAsync(T value)
        {
            int createdBy = GetUserIdFromClaims(_httpContextAccessor) ?? 0;
            var newEntity = BaseEntityFactory<T>.CreateEntity(value, createdBy);

            if (newEntity == null)
            {
                throw new Exception("Failed to create entity.");
            }

            // Ensure the created entity is of type TEntity
            if (!(newEntity is TEntity typedEntity))
            {
                throw new Exception($"Failed to cast the created entity to type {typeof(TEntity)}.");
            }

            _dbContext.Set<TEntity>().Add(typedEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int entityId, T value)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(entityId);

            if (entity == null)
            {
                throw new NotFoundException($"Entity with Id {entityId} not found.");
            }

            entity.Modify(value, GetUserIdFromClaims(_httpContextAccessor) ?? 0);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int entityId)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(entityId);

            if (entity == null)
            {
                throw new NotFoundException($"Entity with Id {entityId} not found.");
            }

            entity.Delete(GetUserIdFromClaims(_httpContextAccessor) ?? 0);
            await _dbContext.SaveChangesAsync();
        }

        private int? GetUserIdFromClaims(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var userIdClaim = httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            throw new Exception("UserNotFound");
        }

    }
    
}


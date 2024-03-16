using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestTaskOmega.Application.Exeptions;
using TestTaskOmega.DataAccess;
using TestTaskOmega.Domain;

namespace TestTaskOmega.Application.RepositoryPattern
{
    public class Repository<TEntity, TEntityHistory> : IRepository<TEntity, TEntityHistory>
        where TEntity : BaseEntity
        where TEntityHistory : BaseEntityHistory
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public Repository(ApplicationDbContext dbContext,
                            IHttpContextAccessor httpContextAccessor,
                            IMapper mapper)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
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

        public async Task<IEnumerable<TEntityHistory>> GetAllHistoryByIdSortedByLatestAsync(int id)
        {
            return await _dbContext.Set<TEntityHistory>()
                .Where(history => history.EntityId == id)
                .OrderByDescending(history => GetLatestDate(history))
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task CreateAsync(TEntity entity, TEntityHistory entityHistory)
        {
            var userId = GetUserIdFromClaims(_httpContextAccessor);
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = userId.HasValue ? userId.Value : 0;
            entityHistory = _mapper.Map<TEntityHistory>(entity);
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.Set<TEntityHistory>().AddAsync(entityHistory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity, TEntityHistory entityHistory)
        {
            var existingEntity = await _dbContext.Set<TEntity>().FindAsync(entity.Id);
            if (existingEntity != null)
            {
                var modificatonTime = DateTime.UtcNow;
                var userId = GetUserIdFromClaims(_httpContextAccessor);
                entityHistory.EntityId = existingEntity.Id;
                entityHistory.ModifiedAt = modificatonTime;
                entityHistory.ModifiedBy = userId.HasValue ? userId.Value : 0;

                existingEntity.ModifiedAt = modificatonTime;
                existingEntity.ModifiedBy = userId.HasValue ? userId.Value : 0;
                _dbContext.Set<TEntityHistory>().Add(entityHistory);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(TEntity entity, TEntityHistory entityHistory)
        {
            var existingEntity = await _dbContext.Set<TEntity>().FindAsync(entity.Id);
            if (existingEntity != null)
            {
                var userId = GetUserIdFromClaims(_httpContextAccessor);
                entityHistory.EntityId = existingEntity.Id;
                entityHistory.DeletedAt = DateTime.UtcNow;
                entityHistory.DeletedBy = userId.HasValue ? userId.Value : 0;
                _dbContext.Set<TEntityHistory>().Add(entityHistory);
                _dbContext.Set<TEntity>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        private int? GetUserIdFromClaims(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var userIdClaim = httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return null;
        }

        private DateTime GetLatestDate(TEntityHistory history)
        {
            return new[]
            {
                history.CreatedAt,
                history.ModifiedAt,
                history.DeletedAt
            }
            .Max();
        }
    }
}


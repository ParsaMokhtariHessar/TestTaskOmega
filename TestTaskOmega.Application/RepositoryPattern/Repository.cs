using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
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
                            IMapper mapper
                            )
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public TEntity GetById(int id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);

            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found.");
            }

            return entity;
        }
        public TEntity GetByCreationDate(DateTime creationDate)
        {
            var entity = _dbContext.Set<TEntity>()
                .FirstOrDefault(e => e.CreatedAt.Date == creationDate.Date);

            if (entity == null)
            {
                throw new NotFoundException($"Entity with creationDate {creationDate} not found.");
            }

            return entity;
        }

        public IEnumerable<TEntityHistory> GetAllHistoryByIdSortedByLatest(int id)
        {
            return _dbContext.Set<TEntityHistory>()
                .Where(history => history.EntityId == id)
                .OrderByDescending(history => GetLatestDate(history))
                .ToList();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public void Create(TEntity entity, TEntityHistory entityHistory)
        {
            var userId = GetUserIdFromClaims(_httpContextAccessor);
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = userId.HasValue ? userId.Value : 0;
            entityHistory = _mapper.Map<TEntityHistory>(entity);
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.Set<TEntityHistory>().Add(entityHistory);
        }

        public void Update(TEntity entity, TEntityHistory entityHistory)
        {
            // Retrieve the existing entity from the database
            var existingEntity = _dbContext.Set<TEntity>().Find(entity.Id);
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
                _dbContext.SaveChanges();
            }
        }

        public void Delete(TEntity entity, TEntityHistory entityHistory)
        {
            // Retrieve the existing entity from the database
            var existingEntity = _dbContext.Set<TEntity>().Find(entity.Id);
            if (existingEntity != null)
            {
                var userId = GetUserIdFromClaims(_httpContextAccessor);
                entityHistory.EntityId = existingEntity.Id;
                entityHistory.DeletedAt = DateTime.UtcNow;
                entityHistory.DeletedBy = userId.HasValue ? userId.Value : 0;
                _dbContext.Set<TEntityHistory>().Add(entityHistory);
                _dbContext.Set<TEntity>().Remove(entity);
            }
        }

        private int? GetUserIdFromClaims(IHttpContextAccessor httpContextAccessor)
        {
            // Retrieve user ID from claims
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
            // Assuming BaseEntityHistory has CreatedAt, ModifiedAt, and DeletedAt properties
            return new[]
            {
                history.CreatedAt,
                history.ModifiedAt,
                history.DeletedAt
            }
            .Max(); // Get the maximum date among the three properties
        }

    }
}


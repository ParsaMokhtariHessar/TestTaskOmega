using AutoMapper;
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
            private readonly IMapper _mapper;

            public Repository(ApplicationDbContext dbContext,
                                IHttpContextAccessor httpContextAccessor,
                                IMapper mapper)
            {
                _dbContext = dbContext;
                _httpContextAccessor = httpContextAccessor;
                _mapper = mapper;
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

            public async Task<IEnumerable<Modifications<T>>> GetHistory(TEntity entity)
            {
                return await Task.FromResult(entity.Modifications);
            }

        public async Task CreateAsync(TEntity entity)
            {
                var userId = GetUserIdFromClaims(_httpContextAccessor);
                entity.CreatedAt = DateTime.UtcNow;
                entity.CreatedBy = userId ?? 0;
                _dbContext.Set<TEntity>().Add(entity);
                await _dbContext.SaveChangesAsync();
            }

            public async Task UpdateAsync(TEntity entity)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }

            public async Task DeleteAsync(TEntity entity)
            {
                entity.Delete(GetUserIdFromClaims(_httpContextAccessor) ?? 0);
                _dbContext.Entry(entity).State = EntityState.Modified;
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
                return null;
            }
        }
    
}


using Microsoft.EntityFrameworkCore;
using TestTaskOmega.Application.Exeptions;
using TestTaskOmega.DataAccess;
using TestTaskOmega.Domain;
using TestTaskOmega.Identity.IdentityModels;
using TestTaskOmega.Identity.IdentityServices.UserService;

namespace TestTaskOmega.Application.RepositoryPattern
{
    public class Repository<TEntity, T> : IRepository<TEntity, T> where TEntity : BaseEntity<T> , new()
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserService _userService;

        public Repository(ApplicationDbContext dbContext,
            IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().Where(e => !(e.IsDeleted)).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllDeletedAsync()
        {
            return await _dbContext.Set<TEntity>().Where(e => e.IsDeleted).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            return entity ?? throw new NotFoundException($"Entity with ID {id} not found.");
        }

        public async Task<IEnumerable<TEntity>> GetByCreationDateAsync(DateTime creationDate)
        {
            var entity = await _dbContext.Set<TEntity>()
                .Where(e => e.CreatedAt.Date == creationDate.Date).ToListAsync();
            return entity ?? throw new NotFoundException($"Entity with creationDate {creationDate} not found.");
        }

        public async Task<IEnumerable<EntityModification<T>>> GetHistory(int entityId)
        {
            var entity = await GetByIdAsync(entityId);
            return entity.History;
                
        }

        public async Task CreateAsync(T value)
        {
            ApplicationUser createdBy =  await FindUserAsync();
            var newEntity = new TEntity();
            newEntity.Create(value, createdBy);

            if (newEntity == null)
            {
                throw new Exception("Failed to create entity.");
            }

            _dbContext.Set<TEntity>().Add(newEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int entityId, T value)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(entityId);

            if (entity == null)
            {
                throw new NotFoundException($"Entity with Id {entityId} not found.");
            }
            ApplicationUser ModifiedBy = await FindUserAsync();
            entity.Modify(value, ModifiedBy);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int entityId)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(entityId);

            if (entity == null)
            {
                throw new NotFoundException($"Entity with Id {entityId} not found.");
            }

            ApplicationUser DeletedBy = await FindUserAsync();
            entity.Delete(DeletedBy);
            await _dbContext.SaveChangesAsync();
        }
        private async Task<ApplicationUser> FindUserAsync()
        {
            return await _userService.GetUserFromClaimsAsync() ?? throw new Exception("User Not Found!");
        }


    }
    
}


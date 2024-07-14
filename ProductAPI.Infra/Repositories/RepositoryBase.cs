using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductAPI.Domain.Contract.Repositories;
using System.Linq.Expressions;

namespace ProductAPI.Infra.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly ProductAPIContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public RepositoryBase(ProductAPIContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual async Task<EntityEntry> AddAsync(TEntity obj)
        {
            return await DbSet.AddAsync(obj);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual async Task RemoveAsync(long id)
        {
            DbSet.Remove(await DbSet.FindAsync(id));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Db.SaveChangesAsync();
        }

        public async Task<ICollection<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetAll();

            if (includes.Any())
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public async Task<ICollection<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetAll();

            if (includes.Any())
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.Where(predicate).ToListAsync();
        }

        public virtual void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

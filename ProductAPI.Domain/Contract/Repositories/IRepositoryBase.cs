using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace ProductAPI.Domain.Contract.Repositories
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        Task<EntityEntry> AddAsync(TEntity obj);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> GetByIdAsync(long id);
        void Update(TEntity obj);
        Task RemoveAsync(long id);
        Task<int> SaveChangesAsync();
        Task<ICollection<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> GetAll();
        Task<ICollection<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    }
}

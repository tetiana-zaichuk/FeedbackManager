using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FeedbackManager.DataAccessLayer.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null);

        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(TKey id);
    }
}

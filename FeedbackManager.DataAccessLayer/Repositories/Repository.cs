using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FeedbackManager.DataAccessLayer.Data;
using FeedbackManager.DataAccessLayer.Interfaces;

namespace FeedbackManager.DataAccessLayer.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected readonly FeedbackManagerDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(FeedbackManagerDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var findEntity = await DbSet.FindAsync(entity.Id);
            Context.Entry(findEntity).State = EntityState.Detached;
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }

        public async Task DeleteAsync(TKey id) => Delete(await DbSet.FindAsync(id));

        public void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }

            try
            {
                DbSet.Remove(entityToDelete);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}

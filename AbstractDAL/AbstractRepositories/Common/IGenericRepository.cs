using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Journal.AbstractDAL.AbstractRepositories.Common
{
    public interface IGenericRepository<TEntity, TKey>
    {
        IEnumerable<TEntity> GetAll(
             Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             int? skip = null,
             int? take = null,
             params Expression<Func<TEntity, object>>[] includeProperties
             );

        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includeProperties
            );

        TEntity GetSingleBy(TKey id);
        Task<TEntity> GetSingleByIdAsync(TKey id);

        TEntity GetFirstOrDefault(
                Expression<Func<TEntity, bool>> filter = null,
                params Expression<Func<TEntity, object>>[] includeProperties
                );

        Task<TEntity> GetFirstOrDefaultAsync(
                Expression<Func<TEntity, bool>> filter = null,
                params Expression<Func<TEntity, object>>[] includeProperties
                );
       
        void Insert(TEntity entity);
        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] includeProperties);

        Task Delete(TKey id);
        void Delete(TEntity entity);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDAL.AbstractRepositories.Common
{
    public interface IGenericAsyncRepository<TEntity, TKey>
    {
      
        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, object>> orderBy = null,
            bool orderDesc = false,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includeProperties
            );
     
        Task<TEntity> GetFirstOrDefaultAsync(
                Expression<Func<TEntity, bool>> filter = null,
                params Expression<Func<TEntity, object>>[] includeProperties
                );

        void InsertOnCommit(TEntity entity);
        void UpdateOnCommit(TEntity entity, params Expression<Func<TEntity, object>>[] includeProperties);

        Task DeleteOnCommit(TKey id);
        void DeleteOnCommit(TEntity entity);

        Task CommitAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AbstractDAL.AbstractRepositories.Common
{
    public interface ISyncEntityReader<TEntity>
    {
        IEnumerable<TEntity> GetAll(
             Expression<Func<TEntity, bool>> filter = null,
             Expression<Func<TEntity, object>> orderBy = null,
             bool orderDesc = false,
             int? skip = null,
             int? take = null,
             params Expression<Func<TEntity, object>>[] includeProperties
             );

        TEntity GetFirstOrDefault(
                Expression<Func<TEntity, bool>> filter = null,
                params Expression<Func<TEntity, object>>[] includeProperties
                );
    }
}

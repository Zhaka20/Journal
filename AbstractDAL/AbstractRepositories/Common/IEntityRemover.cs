using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDAL.AbstractRepositories.Common
{
    public interface IEntityRemover<TEntity>
    {
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> filter);
    }
}

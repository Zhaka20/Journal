using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDAL.AbstractRepositories.Common
{
    public interface IEntityUpdater<TEntity>
    {
        void Update(TEntity entity);
    }
}

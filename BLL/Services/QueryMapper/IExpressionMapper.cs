using Journal.AbstractBLL.AbstractServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.QueryMapper
{
    public interface IQueryExpressionBuilder<TEntityDTO,TEntity>
    {
        Expression<Func<TEntity, TResult>> GetFilter<TReturn,TResult>(Expression<Func<TEntityDTO, TReturn>> expression);
        Expression<Func<TEntity, TResult>> GetOrderByExpression<TReturn,TResult>(Expression<Func<TEntityDTO, SortDirection, TReturn>> expression);
        Expression<Func<TEntity, object>> GetIncludePropertyExpressions(Expression<Func<TEntityDTO, object>>[] includeProperties);
    }
}

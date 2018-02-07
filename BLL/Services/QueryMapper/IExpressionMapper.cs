using Journal.AbstractBLL.AbstractServices.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BLL.Services.QueryMapper
{
    public interface IQueryExpressionBuilder<TEntityDTO,TEntity>
    {
        Expression<Func<TEntity, bool>> GetFilterExpression(Expression<Func<TEntityDTO, bool>> expression);
        //Expression<Func<TEntity, TResult>> GetOrderByExpression<TParamReturn,TResult>(Expression<Func<TEntityDTO, SortDirection, TParamReturn>> expression);
        Expression<Func<TEntity, object>> GetOrderByExpression(Expression<Func<TEntityDTO, SortDirection, object>> expression);
        Expression<Func<TEntity, object>>[] GetIncludePropertyExpressions(params Expression<Func<TEntityDTO, object>>[] includeProperties);
    }
}

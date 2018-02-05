﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Journal.AbstractBLL.AbstractServices.Common
{
    public interface IBasicDTOService<TEntity, TKey>
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

        Task<TEntity> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includedProperties);
        void Update(TEntity dto, params Expression<Func<TEntity, object>>[] includedProperties);
        void Create(TEntity dto);
        void Delete(TEntity dto);
        Task DeleteByIdAsync(TKey id);
        Task SaveChangesAsync();

    }
}

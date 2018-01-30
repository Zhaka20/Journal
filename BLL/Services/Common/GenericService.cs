﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Journal.AbstractDAL.AbstractRepositories.Common;
using BLL.Services.Common;

namespace Journal.BLL.Services.Concrete.Common
{
    public abstract class GenericService<TEntity, TKey> : IGenericService<TEntity, TKey>, IDisposable
    {
        protected readonly IGenericRepository<TEntity, TKey> repository;

        public GenericService(IGenericRepository<TEntity,TKey> repository)
        {
            if(repository == null)
            {
                throw new ArgumentNullException("repository argument cannot be null");
            }
            this.repository = repository;
        }


        public virtual void Create(TEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("Entity to create cannot be null");
            }
            repository.Insert(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity to delete cannot be null");
            }
            repository.Delete(entity);
        }

        public virtual async Task DeleteByIdAsync(TKey id)
        {
            var entity = await repository.GetSingleByIdAsync(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? skip = default(int?), int? take = default(int?), params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return repository.GetAll(filter, orderBy, skip, take, includeProperties);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? skip = default(int?), int? take = default(int?), params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await repository.GetAllAsync(filter, orderBy, skip, take, includeProperties);
        }

        public virtual Task<TEntity> GetByIdAsync(TKey id)
        {
            return repository.GetSingleByIdAsync(id);
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return repository.GetFirstOrDefault(filter, includeProperties);
        }

        public virtual Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
           return repository.GetFirstOrDefaultAsync(filter, includeProperties);
        }

        public virtual void Update(TEntity entity, params Expression<Func<TEntity, object>>[] updateProperties)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("Entity to update cannot be null");
            }
            if(updateProperties != null)
            {
                repository.UpdateSelectedProperties(entity, updateProperties);
            }
            else
            {
                repository.Update(entity);
            }
        }
        public virtual Task SaveChangesAsync()
        {
            return repository.SaveChangesAsync();
        }

        public virtual void Dispose()
        {
            IDisposable disposable = repository as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        public Task<TEntity> GetByCompositeKeysAsync(params object[] keys)
        {
            return repository.GetSingleByIdAsync(keys);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Journal.DAL.Context;
using System.Data.Entity;
using Journal.AbstractDAL.AbstractRepositories.Common;

namespace Journal.DAL.Repositories.Common
{
    public abstract class GenericRepository<TEntity, TKey> : IDisposable, IGenericRepository<TEntity, TKey> where TEntity : class
                            
    {
        protected readonly ApplicationDbContext db;
        protected readonly DbSet<TEntity> dbSet;

        public GenericRepository(ApplicationDbContext db)
        {
            if(db == null)
            {
                throw new ArgumentNullException("argument db cannot be null");
            }
            this.db = db;
            dbSet = db.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity,
            bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;
            GetQueryable(filter, orderBy, skip, take);
            return query.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;
            GetQueryable(filter,orderBy,skip,take);
            return await query.ToListAsync();
        }

        public virtual TEntity GetSingleById(TKey id)
        {
            return dbSet.Find(id);
        }
        public virtual Task<TEntity> GetSingleByIdAsync(TKey id)
        {
            return dbSet.FindAsync(id);
        }
        public virtual void Insert(TEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity argument cannot be null");
            }
            dbSet.Add(entity);
        }
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity argument cannot be null");
            }

            if (db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            db.Entry(entity).State = EntityState.Modified;
        }
        public virtual void UpdateSelectedProperties(TEntity entity, params Expression<Func<TEntity, object>>[] updateProperties)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity argument cannot be null");
            }

            if (db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            foreach (Expression<Func<TEntity, object>> property in updateProperties)
            {
                db.Entry(entity).Property(property).IsModified = true;
            }
        }
        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity argument cannot be null");
            }

            if (db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
        public virtual async Task Delete(TKey id)
        {
            TEntity entity = await dbSet.FindAsync(id);
            if(entity != null)
            {
                Delete(entity);
            }
        }
        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }
        public virtual Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).CountAsync();
        }
        public virtual bool GetExists(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }
        public virtual Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).AnyAsync();
        }
        public virtual void SaveChanges()
        {
            db.SaveChanges();
        }
        public virtual Task SaveChangesAsync()
        {
            return db.SaveChangesAsync();
        }

        protected virtual IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity,
            bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            foreach (Expression<Func<TEntity, object>> include in includeProperties)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            if(includeProperties != null)
            {
                foreach (Expression<Func<TEntity, object>> include in includeProperties)
                    query = query.Include(include);
            }           
            return query.FirstOrDefault(filter);
        }

        public virtual Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            if (includeProperties != null)
            {
                foreach (Expression<Func<TEntity, object>> include in includeProperties)
                    query = query.Include(include);
            }
            return query.FirstOrDefaultAsync(filter);
        }

        public virtual void Dispose()
        {
            db.Dispose();
        }

        public virtual TEntity GetSingleById(params object[] keys)
        {
            return dbSet.Find(keys);
        }

        public virtual Task<TEntity> GetSingleByIdAsync(params object[] keys)
        {
            return dbSet.FindAsync(keys);
        }
    }
}

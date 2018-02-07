using System;
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


        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
                                                   Expression<Func<TEntity, object>> orderBy = null,
                                                   bool orderDesc = false,
                                                   int? skip = null,
                                                   int? take = null,
                                                   params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;
            GetQueryable(filter, orderBy, orderDesc, skip, take);
            return query.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null,
                                                                    Expression<Func<TEntity, object>> orderBy = null,
                                                                    bool orderDesc = false,
                                                                    int? skip = null,
                                                                    int? take = null,
                                                                    params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;
            GetQueryable(filter,orderBy,orderDesc,skip,take);
            return await query.ToListAsync();
        }
     
        public virtual void InsertOnCommit(TEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity argument cannot be null");
            }
            dbSet.Add(entity);
        }

        public virtual void UpdateOnCommit(TEntity entity, params Expression<Func<TEntity, object>>[] updateProperties)
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
        public virtual void DeleteOnCommit(TEntity entity)
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
        public virtual async Task DeleteOnCommit(TKey id)
        {
            TEntity entity = await dbSet.FindAsync(id);
            if(entity != null)
            {
                DeleteOnCommit(entity);
            }
        }
       
        public virtual void Commit()
        {
            db.SaveChanges();
        }
        public virtual Task CommitAsync()
        {
            return db.SaveChangesAsync();
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            if(includeProperties != null)
            {
                foreach (Expression<Func<TEntity, object>> include in includeProperties)
                {
                    query = query.Include(include);
                }
            }           
            return query.FirstOrDefault(filter);
        }

        public virtual Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            if (includeProperties != null)
            {
                foreach (Expression<Func<TEntity, object>> include in includeProperties)
                {
                    query = query.Include(include);
                }
            }
            return query.FirstOrDefaultAsync(filter);
        }

        protected internal virtual IQueryable<TEntity> GetQueryable(
         Expression<Func<TEntity, bool>> filter = null,
         Expression<Func<TEntity, object>> orderBy = null,
         bool orderDesc = false,
         int? skip = null,
         int? take = null,
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
                if (orderDesc)
                {
                    query = query.OrderByDescending(orderBy);
                }
                query = query.OrderBy(orderBy);
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

        public virtual void Dispose()
        {
            db.Dispose();
        }

    }
}

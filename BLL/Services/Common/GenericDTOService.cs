using BLL.Services.Common.Abstract;
using Journal.AbstractBLL.AbstractServices.Common;
using Journal.AbstractDAL.AbstractRepositories.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;

namespace BLL.Services.Common
{
    public abstract class GenericDTOService<TEntityDTO, TEntity, TKey> : IBasicDTOService<TEntityDTO, TKey>
    {
        protected readonly IGenericRepository<TEntity, TKey> currentEntityRepository;
        protected readonly IObjectToObjectMapper mapper;

        public GenericDTOService(IGenericRepository<TEntity, TKey> repository, IObjectToObjectMapper mapper)
        {
            this.currentEntityRepository = repository;
            this.mapper = mapper;
        }

        public void Create(TEntityDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("dto");
            }
            var entity = mapper.Map<TEntityDTO, TEntity>(dto);
            if (entity == null)
            {
                throw new NullReferenceException("Could not convert dto argument to its corresponding entity.");
            }
            currentEntityRepository.Insert(entity);
        }

        public void Delete(TEntityDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("dto");
            }
            var entity = mapper.Map<TEntityDTO, TEntity>(dto);
            if (entity == null)
            {
                throw new NullReferenceException("Could not convert dto argument to its corresponding entity.");
            }
            currentEntityRepository.Delete(entity);
        }

        public async Task DeleteByIdAsync(TKey id)
        {
            var entity = await currentEntityRepository.GetSingleByIdAsync(id);
            currentEntityRepository.Delete(entity);
        }


        public IEnumerable<TEntityDTO> GetAll(Expression<Func<TEntityDTO, bool>> filter = null, 
                                              Func<IQueryable<TEntityDTO>, IOrderedQueryable<TEntityDTO>> orderBy = null,
                                              int? skip = null, int? take = null, 
                                              params Expression<Func<TEntityDTO, object>>[] includeProperties)
        {
            IEnumerable<TEntityDTO> result = new List<TEntityDTO>();
            var entities = currentEntityRepository.GetAll(filter,orderBy,skip,take,includeProperties);
            if (entities == null)
            {
                return result;
            }
            result = mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityDTO>>(entities);
            return result;
        }

        public Task<IEnumerable<TEntityDTO>> GetAllAsync(Expression<Func<TEntityDTO, bool>> filter = null, 
                                                         Func<IQueryable<TEntityDTO>, IOrderedQueryable<TEntityDTO>> orderBy = null,
                                                         int? skip = null, int? take = null,
                                                         params Expression<Func<TEntityDTO, object>>[] includeProperties)
        {
            IEnumerable<TEntityDTO> result = new List<TEntityDTO>();
            var entities = currentEntityRepository.GetAll(filter, orderBy, skip, take, includeProperties);
            if (entities == null)
            {
                return result;
            }
            result = mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityDTO>>(entities);
            return result;
        }

        public async Task<TEntityDTO> GetByIdAsync(TKey id)
        {
            var entity = await currentEntityRepository.GetSingleByIdAsync(id);
            if (entity == null)
            {
                return default(TEntityDTO);
            }
            var result = mapper.Map<TEntity, TEntityDTO>(entity);
            return result;
        }

        public Task<TEntityDTO> GetByIdAsync(TKey id, params Expression<Func<TEntityDTO, object>>[] includedProperties)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            return currentEntityRepository.SaveChangesAsync();
        }

        public void Update(TEntityDTO dto, params Expression<Func<TEntityDTO, object>>[] includedProperties)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntityDTO dto, params Expression<Func<TEntity, object>>[] includedProperties)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("dto");
            }
            var entity = mapper.Map<TEntityDTO, TEntity>(dto);
            if (entity == null)
            {
                throw new NullReferenceException("Could not convert dto argument to its corresponding entity.");
            }
            currentEntityRepository.Update(entity);
        }

        protected 
    }
}

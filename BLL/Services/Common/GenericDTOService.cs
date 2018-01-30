using BLL.Services.Common.Abstract;
using Journal.AbstractBLL.AbstractServices.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Common
{
    public abstract class GenericDTOService<TEntityDTO, TEntity, TKey> : IBasicDTOService<TEntityDTO, TKey>
    {
        protected readonly IGenericService<TEntity, TKey> entityService;
        protected readonly IObjectToObjectMapper mapper;

        public GenericDTOService(IGenericService<TEntity, TKey> entityService, IObjectToObjectMapper mapper)
        {
            this.entityService = entityService;
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
            entityService.Create(entity);
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
            entityService.Delete(entity);
        }

        public async Task DeleteByIdAsync(TKey id)
        {
            var entity = await entityService.GetByIdAsync(id);
            entityService.Delete(entity);
        }

        public IEnumerable<TEntityDTO> GetAll()
        {
            IEnumerable<TEntityDTO> result = new List<TEntityDTO>();
            var entities = entityService.GetAll();
            if (entities == null)
            {
                return result;
            }
            result = mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityDTO>>(entities);
            return result;
        }

        public async Task<IEnumerable<TEntityDTO>> GetAllAsync()
        {
            IEnumerable<TEntityDTO> result = new List<TEntityDTO>();
            var entities = await entityService.GetAllAsync();
            if (entities == null)
            {
                return result;
            }
            result = mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityDTO>>(entities);
            return result;
        }

        public async Task<TEntityDTO> GetByIdAsync(TKey id)
        {
            var entity = await entityService.GetByIdAsync(id);
            if (entity == null)
            {
                return default(TEntityDTO);
            }
            var result = mapper.Map<TEntity, TEntityDTO>(entity);
            return result;
        }

        public Task SaveChangesAsync()
        {
            return entityService.SaveChangesAsync();
        }

        public void Update(TEntityDTO dto)
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
            entityService.Update(entity);
        }
    }
}

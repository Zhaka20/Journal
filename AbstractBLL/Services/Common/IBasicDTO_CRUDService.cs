using System.Collections.Generic;
using System.Threading.Tasks;

namespace Journal.AbstractBLL.AbstractServices.Common
{
    public interface IBasicDTOService<TEntity, TKey>
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        void UpdateMentorsBaseData(TEntity dto);
        void Create(TEntity dto);
        void Delete(TEntity dto);
        Task DeleteByIdAsync(TKey id);
        Task SaveChangesAsync();
    }
}

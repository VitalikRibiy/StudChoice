using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudChoice.BLL.Services
{
    public interface ICrudService<TEntityDTO> where TEntityDTO: class, new()
    {
        Task<TEntityDTO> GetAsync(long id);

        Task<IEnumerable<TEntityDTO>> GetAllAsync();

        Task<IEnumerable<TEntityDTO>> GetRangeAsync(uint offset, uint amount);

        Task<TEntityDTO> CreateAsync(TEntityDTO dto);

        Task<TEntityDTO> UpdateAsync(TEntityDTO dto);

        Task DeleteAsync(long id);

        public void Dispose();
    }
}

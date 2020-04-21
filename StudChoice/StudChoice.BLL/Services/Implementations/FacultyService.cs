using AutoMapper;
using StudChoice.BLL.DTOs;
using StudChoice.BLL.Services.Interfaces;
using StudChoice.DAL.Models;
using StudChoice.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudChoice.BLL.Services.Implementations
{
    public class FacultyService : IFacultyService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public FacultyService(IUnitOfWork unitOfWorkVar, IMapper mapperVar)
        {
            unitOfWork = unitOfWorkVar;
            mapper = mapperVar;
        }

        public async Task<FacultyDTO> CreateAsync(FacultyDTO dto)
        {
            var model = mapper.Map<Faculty>(dto);

            await unitOfWork.FacultyRepository.AddAsync(model);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<FacultyDTO>(model);
        }

        public async Task DeleteAsync(long id)
        {
            unitOfWork.FacultyRepository.Remove(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<FacultyDTO> GetAsync(long id)
        {
            return mapper.Map<FacultyDTO>(await unitOfWork.FacultyRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<FacultyDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await unitOfWork.FacultyRepository.GetRangeAsync(offset, amount);
            return mapper.Map<IEnumerable<FacultyDTO>>(entities);
        }

        public async Task<FacultyDTO> UpdateAsync(FacultyDTO dto)
        {
            var model = mapper.Map<Faculty>(dto);

            unitOfWork.FacultyRepository.Update(model);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<FacultyDTO>(model);
        }

        public void Dispose()
        {
            unitOfWork?.Dispose();
        }

        public async Task<IEnumerable<FacultyDTO>> GetAllAsync()
        {
            var entities = await unitOfWork.FacultyRepository.GetAllAsync();
            return mapper.Map<IEnumerable<FacultyDTO>>(entities);
        }
    }
}

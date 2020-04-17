using AutoMapper;
using StudChoice.BLL.DTOs;
using StudChoice.BLL.Services.Interfaces;
using StudChoice.DAL.Models;
using StudChoice.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudChoice.BLL.Services.Implementations
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
      
        public SubjectService(IUnitOfWork unitOfWorkVar, IMapper mapperVar)
        {
            unitOfWork = unitOfWorkVar;
            mapper = mapperVar;
        }

        public async Task<SubjectDTO> CreateAsync(SubjectDTO dto)
        {
            var model = mapper.Map<Subject>(dto);

            await unitOfWork.SubjectRepository.AddAsync(model);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<SubjectDTO>(model);
        }

        public async Task DeleteAsync(long id)
        {
            unitOfWork.SubjectRepository.Remove(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<SubjectDTO> GetAsync(long id)
        {
            return mapper.Map<SubjectDTO>(await unitOfWork.SubjectRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<SubjectDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await unitOfWork.SubjectRepository.GetRangeAsync(offset, amount);
            return mapper.Map<IEnumerable<SubjectDTO>>(entities);
        }

        public async Task<SubjectDTO> UpdateAsync(SubjectDTO dto)
        {
            var model = mapper.Map<Subject>(dto);

            unitOfWork.SubjectRepository.Update(model);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<SubjectDTO>(model);
        }

        public void Dispose()
        {
            unitOfWork?.Dispose();
        }

        public async Task<IEnumerable<SubjectDTO>> GetAllAsync()
        {
            var entities = await unitOfWork.SubjectRepository.GetAllAsync();
            return mapper.Map<IEnumerable<SubjectDTO>>(entities);
        }
    }
}

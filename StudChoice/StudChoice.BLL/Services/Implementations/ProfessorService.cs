using AutoMapper;
using StudChoice.BLL.DTOs;
using StudChoice.BLL.Services.Interfaces;
using StudChoice.DAL.Models;
using StudChoice.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudChoice.BLL.Services.Implementations
{
    public class ProfessorService : IProfessorService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProfessorService(IUnitOfWork unitOfWorkVar, IMapper mapperVar)
        {
            unitOfWork = unitOfWorkVar;
            mapper = mapperVar;
        }

        public async Task<ProfessorDTO> CreateAsync(ProfessorDTO dto)
        {
            var model = mapper.Map<Professor>(dto);

            await unitOfWork.ProfessorRepository.AddAsync(model);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<ProfessorDTO>(model);
        }

        public async Task DeleteAsync(long id)
        {
            unitOfWork.ProfessorRepository.Remove(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<ProfessorDTO> GetAsync(long id)
        {
            var professor = mapper.Map<ProfessorDTO>(await unitOfWork.ProfessorRepository.GetByIdAsync(id));
            professor.FacultyName = (await unitOfWork.FacultyRepository.GetByIdAsync(professor.FacultyId)).DisplayName;
            professor.CathedraName = (await unitOfWork.CathedraRepository.GetByIdAsync(professor.CathedraId)).DisplayName;
            return professor;
        }

        public async Task<IEnumerable<ProfessorDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await unitOfWork.ProfessorRepository.GetRangeAsync(offset, amount);
            return mapper.Map<IEnumerable<ProfessorDTO>>(entities);
        }

        public async Task<ProfessorDTO> UpdateAsync(ProfessorDTO dto)
        {
            var model = mapper.Map<Professor>(dto);

            unitOfWork.ProfessorRepository.Update(model);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<ProfessorDTO>(model);
        }

        public void Dispose()
        {
            unitOfWork?.Dispose();
        }

        public async Task<IEnumerable<ProfessorDTO>> GetAllAsync()
        {
            var entities = await unitOfWork.ProfessorRepository.GetAllAsync();

            var professors = mapper.Map<IEnumerable<ProfessorDTO>>(entities);

            foreach (var professor in professors)
            {
                professor.FacultyName = (await unitOfWork.FacultyRepository.GetByIdAsync(professor.FacultyId)).DisplayName;
                professor.CathedraName = (await unitOfWork.CathedraRepository.GetByIdAsync(professor.CathedraId)).DisplayName;
            }

            return professors;
        }
    }
}

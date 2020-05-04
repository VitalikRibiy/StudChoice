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

        public async Task DeleteAsync(int id)
        {
            unitOfWork.SubjectRepository.Remove(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<SubjectDTO> GetAsync(int id)
        {
            var subject = mapper.Map<SubjectDTO>(await unitOfWork.SubjectRepository.GetByIdAsync(id));
            subject.FacultyName = (await unitOfWork.FacultyRepository.GetByIdAsync(subject.FacultyId)).DisplayName;
            subject.CathedraName = (await unitOfWork.CathedraRepository.GetByIdAsync(subject.CathedraId)).DisplayName;
            subject.ProfessorFullName = (await unitOfWork.ProfessorRepository.GetByIdAsync(subject.ProfessorId)).FullName;
            return subject;
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

            var subjectDTOs = mapper.Map<IEnumerable<SubjectDTO>>(entities);

            foreach (var subject in subjectDTOs)
            {
                subject.FacultyName = (await unitOfWork.FacultyRepository.GetByIdAsync(subject.FacultyId)).DisplayName;
                subject.CathedraName = (await unitOfWork.CathedraRepository.GetByIdAsync(subject.CathedraId)).DisplayName;
                subject.ProfessorFullName = (await unitOfWork.ProfessorRepository.GetByIdAsync(subject.ProfessorId)).FullName;
            }

            return subjectDTOs;
        }
    }
}

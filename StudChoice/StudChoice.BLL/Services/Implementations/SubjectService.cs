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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SubjectDTO> CreateAsync(SubjectDTO dto)
        {
            var model = _mapper.Map<Subject>(dto);

            await _unitOfWork.SubjectRepository.AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<SubjectDTO>(model);
        }

        public async Task DeleteAsync(long id)
        {
            _unitOfWork.SubjectRepository.Remove(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<SubjectDTO> GetAsync(long id)
        {
            return _mapper.Map<SubjectDTO>(await _unitOfWork.SubjectRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<SubjectDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await _unitOfWork.SubjectRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<SubjectDTO>>(entities);
        }

        public async Task<SubjectDTO> UpdateAsync(SubjectDTO dto)
        {
            var model = _mapper.Map<Subject>(dto);

            _unitOfWork.SubjectRepository.Update(model);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<SubjectDTO>(model);
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}

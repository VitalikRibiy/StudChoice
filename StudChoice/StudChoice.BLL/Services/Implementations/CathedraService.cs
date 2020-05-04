using AutoMapper;
using StudChoice.BLL.DTOs;
using StudChoice.BLL.Services.Interfaces;
using StudChoice.DAL.Models;
using StudChoice.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudChoice.BLL.Services.Implementations
{
    public class CathedraService : ICathedraService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CathedraService(IUnitOfWork unitOfWorkVar, IMapper mapperVar)
        {
            unitOfWork = unitOfWorkVar;
            mapper = mapperVar;
        }

        public async Task<CathedraDTO> CreateAsync(CathedraDTO dto)
        {
            var model = mapper.Map<Cathedra>(dto);

            await unitOfWork.CathedraRepository.AddAsync(model);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<CathedraDTO>(model);
        }

        public async Task DeleteAsync(int id)
        {
            unitOfWork.CathedraRepository.Remove(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<CathedraDTO> GetAsync(int id)
        {
            var cathedra = mapper.Map<CathedraDTO>(await unitOfWork.CathedraRepository.GetByIdAsync(id));
            cathedra.FacultyName = (await unitOfWork.FacultyRepository.GetByIdAsync(cathedra.FacultyId)).DisplayName;
            return cathedra;
        }

        public async Task<IEnumerable<CathedraDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await unitOfWork.CathedraRepository.GetRangeAsync(offset, amount);
            return mapper.Map<IEnumerable<CathedraDTO>>(entities);
        }

        public async Task<CathedraDTO> UpdateAsync(CathedraDTO dto)
        {
            var model = mapper.Map<Cathedra>(dto);

            unitOfWork.CathedraRepository.Update(model);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<CathedraDTO>(model);
        }

        public void Dispose()
        {
            unitOfWork?.Dispose();
        }

        public async Task<IEnumerable<CathedraDTO>> GetAllAsync()
        {
            var entities = await unitOfWork.CathedraRepository.GetAllAsync();

            var cathedras = mapper.Map<IEnumerable<CathedraDTO>>(entities);

            foreach (var cathedra in cathedras)
            {
                cathedra.FacultyName = (await unitOfWork.FacultyRepository.GetByIdAsync(cathedra.FacultyId)).DisplayName;
            }

            return cathedras;
        }
    }
}

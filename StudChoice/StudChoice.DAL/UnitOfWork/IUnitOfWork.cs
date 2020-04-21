using StudChoice.DAL.Repositories.RepositoryInterfaces;
using System;
using System.Threading.Tasks;

namespace StudChoice.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public ISubjectRepository SubjectRepository { get; }

        public IFacultyRepository FacultyRepository { get; }

        public IProfessorRepository ProfessorRepository { get; }

        public ICathedraRepository CathedraRepository { get; }

        public Task SaveChangesAsync();
    }
}

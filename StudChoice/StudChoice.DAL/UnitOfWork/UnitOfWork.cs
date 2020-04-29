using StudChoice.DAL.EF;
using StudChoice.DAL.Repositories.RepositoryInterfaces;
using System;
using System.Threading.Tasks;

namespace StudChoice.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudChoiceContext context;

        private bool disposed = false;

        public ISubjectRepository SubjectRepository { get; }

        public IFacultyRepository FacultyRepository { get; }

        public IProfessorRepository ProfessorRepository { get; }

        public ICathedraRepository CathedraRepository { get; }

        public UnitOfWork(
            StudChoiceContext contextVar,
            ISubjectRepository subjectRepository,
            IFacultyRepository facultyRepository,
            IProfessorRepository professorRepository,
            ICathedraRepository cathedraRepository
        )
        {
            context = contextVar;
            SubjectRepository = subjectRepository;
            FacultyRepository = facultyRepository;
            ProfessorRepository = professorRepository;
            CathedraRepository = cathedraRepository;
        }

        public Task SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                this.disposed = true;
            }
        }
    }
}

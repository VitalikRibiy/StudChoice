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

        public UnitOfWork(StudChoiceContext contextVar, ISubjectRepository subjectRepository)
        {
            context = contextVar;
            SubjectRepository = subjectRepository;
        }

        public ISubjectRepository SubjectRepository { get; }

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

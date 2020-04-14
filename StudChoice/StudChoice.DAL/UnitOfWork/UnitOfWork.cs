using Microsoft.EntityFrameworkCore;
using StudChoice.DAL.EF;
using StudChoice.DAL.Models;
using StudChoice.DAL.Repositories;
using StudChoice.DAL.Repositories.RepositoryImplementations;
using StudChoice.DAL.Repositories.RepositoryInterfaces;
using System;
using System.Threading.Tasks;

namespace StudChoice.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private StudChoiceContext _context;
        
        public ISubjectRepository SubjectRepository { get; }

        
        public UnitOfWork(
            StudChoiceContext context,
            ISubjectRepository subjectRepository)
        {
            _context = context;
            SubjectRepository = subjectRepository;
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}

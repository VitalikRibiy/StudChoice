using Microsoft.EntityFrameworkCore;
using StudChoice.DAL.EF;
using StudChoice.DAL.Models;
using StudChoice.DAL.Repositories;
using StudChoice.DAL.Repositories.RepositoryImplementations;
using System;

namespace StudChoice.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private EFDBContext db;
        private SubjectRepository subjectRepository;


        public UnitOfWork(DbContextOptions<EFDBContext> options)
        {
            this.db = new EFDBContext(options);
        }

        public void Commit()
        {
            db.SaveChanges();
        }

        public IBaseRepository<Subject> Subjects
        {
            get
            {
                if (this.subjectRepository == null)
                {
                    this.subjectRepository = new SubjectRepository(db);
                }

                return subjectRepository;
            }
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
                    db.Dispose();
                }
                this.disposed = true;
            }
        }


        public void save()
        {
            throw new System.NotImplementedException();
        }
    }
}

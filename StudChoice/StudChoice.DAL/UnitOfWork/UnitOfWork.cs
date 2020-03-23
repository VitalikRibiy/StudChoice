using StudChoice.DAL.EF;
using StudChoice.DAL.Models;
using StudChoice.DAL.Repositories;
using System;

namespace StudChoice.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        private SubjectRepository subjectRepository;

        public UnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
        }
        public IBaseRepository<Subject> Subjects {
            get
            {
                if(subjectRepository == null)
                {
                    subjectRepository = new SubjectRepository(db);
                }

                return subjectRepository;
            }
        }

        public  void Dispose()
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

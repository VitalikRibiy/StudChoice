
using StudChoice.DAL.EF;
using StudChoice.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace StudChoice.DAL.Repositories
{
    class SubjectRepository : IBaseRepository<Subject>
    {
        private ApplicationContext db;

        public SubjectRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public void Create(Subject item)
        {
           db.Subjects.Add(item);
        }

        public void Delete(int id)
        {
            Subject subj = db.Subjects.Find(id);
            if (subj != null)
                db.Subjects.Remove(subj);
        }

        public IEnumerable<Subject> Find(Func<Subject, bool> predicate)
        {
            return db.Subjects.Where(predicate).ToList();
        }

        public Subject Get(long id)
        {
            return db.Subjects.Find(id);
        }

        public IEnumerable<Subject> GetAll()
        {
            return db.Subjects;
        }

        public void Update(Subject item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}

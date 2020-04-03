using StudChoice.DAL.Models;
using StudChoice.DAL.Repositories.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using StudChoice.Areas.Identity.Data;
using StudChoice.DAL.EF;
using System.Linq;

namespace StudChoice.DAL.Repositories.RepositoryImplementations
{
    public class SubjectRepository : IBaseRepository<Subject>
    {

        private EFDBContext db;

        public SubjectRepository(EFDBContext context)
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
            var data = db.Subjects;
            return data;
        }

        public void Update(Subject item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}

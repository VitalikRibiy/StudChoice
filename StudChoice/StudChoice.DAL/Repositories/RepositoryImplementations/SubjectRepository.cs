using StudChoice.DAL.Models;
using StudChoice.DAL.Repositories.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using StudChoice.Areas.Identity.Data;
using StudChoice.DAL.EF;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace StudChoice.DAL.Repositories.RepositoryImplementations
{
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(EFDBContext dbContext):base(dbContext)
        {
        }
    }
}

using StudChoice.DAL.EF;
using StudChoice.DAL.Models;
using StudChoice.DAL.Repositories.RepositoryInterfaces;

namespace StudChoice.DAL.Repositories.RepositoryImplementations
{
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(StudChoiceContext dbContext) : base(dbContext)
        {
        }
    }
}

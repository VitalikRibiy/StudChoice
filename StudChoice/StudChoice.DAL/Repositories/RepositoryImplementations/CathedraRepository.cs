using StudChoice.DAL.EF;
using StudChoice.DAL.Models;
using StudChoice.DAL.Repositories.RepositoryInterfaces;

namespace StudChoice.DAL.Repositories.RepositoryImplementations
{
    public class CathedraRepository : BaseRepository<Cathedra>, ICathedraRepository
    {
        public CathedraRepository(StudChoiceContext dbContext) : base(dbContext)
        {
        }
    }
}
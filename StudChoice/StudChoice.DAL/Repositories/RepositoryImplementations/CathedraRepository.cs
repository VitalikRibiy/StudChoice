using StudChoice.DAL.EF;
using StudChoice.DAL.Models;
using StudChoice.DAL.Repositories.RepositoryInterfaces;

namespace StudChoice.DAL.Repositories.RepositoryImplementations
{
    class CathedrRepository : BaseRepository<Cathedra>, ICathedraRepository
    {
        public CathedrRepository(StudChoiceContext dbContext) : base(dbContext)
        {
        }
    }
}
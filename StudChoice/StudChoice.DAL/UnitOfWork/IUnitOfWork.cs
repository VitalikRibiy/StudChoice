using StudChoice.DAL.Models;
using StudChoice.DAL.Repositories;

namespace StudChoice.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IBaseRepository<Subject> Subjects { get; }
        public void save();
    }
}

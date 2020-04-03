using StudChoice.DAL.Models;
using StudChoice.DAL.Repositories;
using System;

namespace StudChoice.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepository<Subject> Subjects { get; }
        public void save();
    }
}

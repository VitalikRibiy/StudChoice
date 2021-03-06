﻿using StudChoice.DAL.EF;
using StudChoice.DAL.Models;
using StudChoice.DAL.Repositories.RepositoryInterfaces;

namespace StudChoice.DAL.Repositories.RepositoryImplementations
{
    public class FacultyRepository : BaseRepository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(StudChoiceContext dbContext) : base(dbContext)
        {
        }
    }
}

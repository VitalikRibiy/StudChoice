﻿using StudChoice.DAL.EF;
using StudChoice.DAL.Models;
using StudChoice.DAL.Repositories.RepositoryInterfaces;

namespace StudChoice.DAL.Repositories.RepositoryImplementations
{
    class ProfessorRepository : BaseRepository<Professor>, IProfessorRepository
    {
        public ProfessorRepository(StudChoiceContext dbContext) : base(dbContext)
        {
        }
    }
}

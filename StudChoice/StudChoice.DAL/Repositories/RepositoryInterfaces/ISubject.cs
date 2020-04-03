using StudChoice.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.DAL.Repositories.RepositoryInterfaces
{
    interface ISubject : IBaseRepository<Subject>
    {
        IEnumerable<Subject> GetAllSubjects();

        Subject GetSubjectById(long id);

        void Createubject(Subject subj);
        void UpdateSubject(Subject subj);
        void DeleteSubject(int id);
    }
}

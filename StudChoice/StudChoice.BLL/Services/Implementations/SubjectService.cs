using StudChoice.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using StudChoice.DAL.Models;
using StudChoice.DAL.Repositories.RepositoryImplementations;
using StudChoice.DAL.EF;

namespace StudChoice.BLL.Services.Implementations
{
    class SubjectService
    {
        IUnitOfWork Database { get; set; }

        public SubjectService(IUnitOfWork db)
        {
            Database = db;
        }
        public void AddSubject(SubjectDTO subjectDTO)
        {
            Subject subj = Database.Subjects.Get(subjectDTO.id);

            if (subj == null)
                throw new ValidationException("Subject not found", "");


            StudChoice.DAL.Models.Subject subject = new Subject
            {
                name = subj.name,
                description = subj.description,
                type = subj.type
            };
            Database.Subjects.Create(subject);
            Database.save();
        }

        public void Dispose()
        {
            Database?.Dispose();
        }

        public SubjectDTO GetSubject(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubjectDTO> GetSubjects()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Subject, SubjectDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Subject>, List<SubjectDTO>>(Database.Subjects.GetAll());
        }
    }
}

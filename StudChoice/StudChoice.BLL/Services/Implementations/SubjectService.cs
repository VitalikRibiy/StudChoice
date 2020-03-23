using AutoMapper;
using StudChoice.BLL.DTOs;
using StudChoice.BLL.Services.Interfaces;
using StudChoice.DAL.Models;
using StudChoice.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudChoice.BLL.Services
{
    public class SubjectService : ISubjectService
    {
        IUnitOfWork Database { get; set; }
        public void AddSubject(SubjectDTO subjectDTO)
        {
            Subject subj = Database.Subjects.Get(subjectDTO.id);

            if (subj == null)
                throw new ValidationException("Subject not found");


            Subject subject = new Subject
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
            throw new NotImplementedException();
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

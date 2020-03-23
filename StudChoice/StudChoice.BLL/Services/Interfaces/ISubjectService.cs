using StudChoice.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.BLL.Services.Interfaces
{
   public interface ISubjectService
    {
        void AddSubject(SubjectDTO subjectDto);
        SubjectDTO GetSubject(int? id);
        IEnumerable<SubjectDTO> GetSubjects();
        void Dispose();
    }
}

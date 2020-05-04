using StudChoice.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.BLL.DTOs
{
   public class SubjectDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public int CathedraId { get; set; }

        public string CathedraName { get; set; }

        public int FacultyId { get; set; }

        public string FacultyName { get; set; }

        public int ProfessorId { get; set; }

        public string ProfessorFullName { get; set; }

        public int MinStudents { get; set; }

        public int MaxStudents { get; set; }

        public int AssignedStudentsCount { get; set; }

        public Course Course { get; set; }
    }
}

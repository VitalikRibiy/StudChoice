using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.BLL.DTOs
{
    public class UserFilterParams
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string TransictionCode { get; set; }
    }
    public class SubjectFilterParams
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string MinStudents { get; set; }
        public string MaxStudents { get; set; }
        public string FacultyName { get; set; }
        public string Professor { get; set; }
    }
    public class FacultyFilterParams
    {
        public string Name { get; set; }
    }
    public class CathedraFilterParams
    {
        public string Name { get; set; }
    }
    public class ProfessorFilterParams
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FacultyName { get; set; }
        public string CathedraName { get; set; }
    }
}

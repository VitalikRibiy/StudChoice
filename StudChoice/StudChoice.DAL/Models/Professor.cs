using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.DAL.Models
{
    public class Professor : BaseModel
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName { get { return LastName + " " + FirstName + " " + MiddleName; } }

        public long FacultyId { get; set; }

        public long CathedraId { get; set; }

        public List<Subject> Subjects { get; set; }
    }
}

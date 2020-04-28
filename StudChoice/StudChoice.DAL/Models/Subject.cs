using System.ComponentModel.DataAnnotations;

namespace StudChoice.DAL.Models
{
    public class Subject : BaseModel
    {
        [Required]
        [Display(Name = "Subject name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Professor")]
        public long ProfessorId { get; set; }

        [Display(Name = "Cathedra")]
        public long CathedraId { get; set; }

        [Display(Name = "Faculty")]
        public long FacultyId { get; set; }

        [Display(Name = "MinStudents")]
        public int MinStudents { get; set; }

        [Display(Name = "MaxStudents")]
        public int MaxStudents { get; set; }

        [Display(Name = "AssignedUsers")]
        public int AssignedStudentsCount { get; set; }
    }
}

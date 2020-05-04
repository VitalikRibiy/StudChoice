using Microsoft.AspNetCore.Identity;

namespace StudChoice.DAL.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public int? FacultyId { get; set; }

        public int? CathedraId { get; set; }

        public int? Dv1Id { get; set; }

        public int? Dv2Id { get; set; }

        public int? Dvvs1Id { get; set; }

        public int? Dvvs2Id { get; set; }

        public Course Course { get; set; }

        public double? AvaragePoints { get; set; }
    }
}
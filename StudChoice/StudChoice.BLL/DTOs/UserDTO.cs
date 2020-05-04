using System;
using System.Collections.Generic;
using System.Text;
using StudChoice.DAL.Models;

namespace StudChoice.BLL.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public int FacultyId { get; set; }

        public string FacultyName { get; set; }

        public int CathedraId { get; set; }

        public string CathedraName { get; set; }

        public int? Dv1Id { get; set; }

        public string Dv1IName { get; set; }

        public int? Dv2Id { get; set; }

        public string Dv2IName { get; set; }

        public int? Dvvs1Id { get; set; }

        public string Dvvs1Name { get; set; }

        public int? Dvvs2Id { get; set; }

        public string Dvvs2Name { get; set; }

        public Course Course { get; set; }

        public double? AvaragePoints { get; set; }
    }
}

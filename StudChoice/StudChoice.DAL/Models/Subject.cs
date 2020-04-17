using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudChoice.DAL.Models
{
    public class Subject : BaseModel
    {
        [Required]
        [Display(Name = "Subject name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Type")]
        public string type { get; set; }

        [Display(Name = "Professor")]
        public Professor Professor { get; set; }

        [Display(Name = "Type")]
        public Cathedra Cathedra { get; set; }
    }
}

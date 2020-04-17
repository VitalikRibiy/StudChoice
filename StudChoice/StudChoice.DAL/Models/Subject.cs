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
        public Professor Professor { get; set; }

        [Display(Name = "Type")]
        public Cathedra Cathedra { get; set; }
    }
}

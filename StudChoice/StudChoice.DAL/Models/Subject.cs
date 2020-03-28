using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudChoice.DAL.Models
{
    public class Subject
    {
        [Required]
        public long id { get; set; }
        
        [Required]
        [Display(Name = "Subject name")]
        public string name { get; set; }

        [Display(Name ="Description")]
        public string description { get; set; }

        [Display(Name ="Type")]
        public string type { get; set; }
    }
}

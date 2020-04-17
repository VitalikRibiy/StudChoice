using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.DAL.Models
{
    public class Cathedra : BaseModel 
    {
        public string DisplayName { get; set; }

        public string Description { get; set; }
        
        public int FacultyId { get; set; }
    }
}

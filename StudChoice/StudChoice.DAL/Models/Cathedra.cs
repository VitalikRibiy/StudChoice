using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.DAL.Models
{
    public class Cathedra : BaseModel 
    {
        public string DisplayName { get; set; }
        
        public Faculty Faculty { get; set; }
    }
}

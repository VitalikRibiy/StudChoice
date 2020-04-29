using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.DAL.Models
{
    public class Faculty : BaseModel
    {
        public string DisplayName { get; set; }

        public string Description { get; set; }

        public List<Cathedra> Cathedras { get; set; }
    }
}

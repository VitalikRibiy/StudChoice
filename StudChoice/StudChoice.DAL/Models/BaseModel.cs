using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudChoice.DAL.Models
{
    class BaseModel
    {
        [Required]
        public long id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudChoice.DAL.Models
{
    public interface IBaseModel
    {
        [Required]
        public long Id { get; set; }
    }
}

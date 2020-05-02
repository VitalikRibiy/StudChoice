using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.BLL.DTOs
{
    public class UserFilterParams
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string TransictionCode { get; set; }
    }
}

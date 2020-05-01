using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudChoice.Models
{
    public class AccountModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string TransictionCode { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

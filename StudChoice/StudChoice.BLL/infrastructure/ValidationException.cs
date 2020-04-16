using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.BLL.Infrastructure
{
   public class ValidationException : Exception
    {      
        public ValidationException(string message, string prop) : base(message)
        {
            Property = prop;
        }

        public string Property { get; protected set; }
    }
}

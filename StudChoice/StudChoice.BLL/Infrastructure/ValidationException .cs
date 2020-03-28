using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.BLL.Infrastructure
{
    class ValidationException : Exception
    {

        public string Property { get; protected set; }
        public ValidationException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}

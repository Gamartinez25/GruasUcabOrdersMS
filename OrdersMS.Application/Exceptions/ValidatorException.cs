using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Exceptions
{
    public class ValidatorException : Exception
    {
        public ValidatorException(string? message) : base(message)
        {
        }

        public ValidatorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

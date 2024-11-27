using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Exceptions
{
    public class NotFoundOrderException : Exception
    {
        public NotFoundOrderException(string? message) : base(message)
        {
        }
    }
}

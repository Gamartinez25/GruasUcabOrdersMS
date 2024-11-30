using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Exceptions
{
    public class InvalidAdditionalCostStateException : Exception
    {
        public InvalidAdditionalCostStateException(string? message) : base(message)
        {
        }
    }
}

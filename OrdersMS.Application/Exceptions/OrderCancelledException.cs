using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Exceptions
{
    public class OrderCancelledException : Exception
    {
        public OrderCancelledException(string? message) : base(message)
        {
        }
    }
}

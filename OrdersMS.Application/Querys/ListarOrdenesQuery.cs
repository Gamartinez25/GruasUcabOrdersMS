using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Querys
{
    public class ListarOrdenesQuery:IRequest<IEnumerable<ListarOrdenesDto>>
    {
    }
}

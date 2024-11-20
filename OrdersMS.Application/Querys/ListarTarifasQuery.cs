using MediatR;
using OrdersMS.Application.Dtos.TarifaDtos;
using OrdersMS.Application.Mappers.TarifaMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Querys
{
    public class ListarTarifasQuery:IRequest<IEnumerable<ListarTarifaDto>>
    {

    }
}

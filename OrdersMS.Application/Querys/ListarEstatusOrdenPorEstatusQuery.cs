using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Querys
{
    public class ListarEstatusOrdenPorEstatusQuery:IRequest<List<EstadoOrden>>
    {
        public string Estatus {  get;  private set; }
        public ListarEstatusOrdenPorEstatusQuery(string estatus)
        {
            Estatus = estatus;
        }

    }
}

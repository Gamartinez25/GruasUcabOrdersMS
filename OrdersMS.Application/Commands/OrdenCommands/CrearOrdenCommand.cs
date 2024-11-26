using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Commands.OrdenCommands
{
    public class CrearOrdenCommand:IRequest
    {

        public CrearOrdenDto CrearOrdenDto { get; private set; }
        public CrearOrdenCommand(CrearOrdenDto crearOrdenDto)
        {
            CrearOrdenDto=crearOrdenDto;
        }

    }
}

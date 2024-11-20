using MediatR;
using OrdersMS.Application.Dtos.TarifaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Commands.TarifaCommands
{
    public class CrearTarifaCommand : IRequest
    {


        public CrearTarifaDto CrearTarifaDto { get; set; }
        public CrearTarifaCommand(CrearTarifaDto crearTarifaDto)
        {
            CrearTarifaDto = crearTarifaDto;
        }
    }
}

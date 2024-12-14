using System;
using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;

namespace OrdersMS.Application.Commands.OrdenCommands
{
    public class ModificarEstatusOrdenCommand:IRequest
    {
        public ModificarEstatusOrdenCommand(ModificarEstatusDto estatusDto)
        {

            EstatusDto = estatusDto;
        }

        public ModificarEstatusDto EstatusDto { get; private set; }
       

    }
}

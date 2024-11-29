using MediatR;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Commands.CostoAdicionalCommands
{
    public class RegistrarCostoAdicionalCommand : IRequest
    {
        public RegistrarCostoAdicionalDto CostoAdicionalDto { get; private set; }
        public RegistrarCostoAdicionalCommand(RegistrarCostoAdicionalDto costoAdicionalDto)
        {
            CostoAdicionalDto = costoAdicionalDto;
        }
    }
}

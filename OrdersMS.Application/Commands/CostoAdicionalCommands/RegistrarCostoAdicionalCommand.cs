using MediatR;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;


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

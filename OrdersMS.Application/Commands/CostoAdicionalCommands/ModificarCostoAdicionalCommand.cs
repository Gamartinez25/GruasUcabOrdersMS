using MediatR;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;

namespace OrdersMS.Application.Commands.CostoAdicionalCommands
{
    public class ModificarCostoAdicionalCommand:IRequest
    {
        public ModificarCostoAdicionalCommand(Guid id, ModificarCostoAdicionalDto costoAdicionalDto)
        {
            Id= id;
            CostoAdicionalDto = costoAdicionalDto;
        }


        public Guid Id { get; private set; }
        public ModificarCostoAdicionalDto CostoAdicionalDto { get; private set; }

    }
}

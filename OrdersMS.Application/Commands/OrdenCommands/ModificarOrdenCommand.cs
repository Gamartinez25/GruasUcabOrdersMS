using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;

namespace OrdersMS.Application.Commands.OrdenCommands
{
    public class ModificarOrdenCommand:IRequest
    {
        public ModificarOrdenDto OrdenDto { get; private set; }
        public Guid Id { get; private set; }
        public ModificarOrdenCommand(ModificarOrdenDto ordenDto, Guid id)
        {
            OrdenDto = ordenDto;
            Id = id;
        }
    }
}

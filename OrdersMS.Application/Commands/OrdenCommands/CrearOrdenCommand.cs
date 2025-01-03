using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;

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

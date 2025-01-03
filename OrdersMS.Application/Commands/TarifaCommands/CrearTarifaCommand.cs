using MediatR;
using OrdersMS.Application.Dtos.TarifaDtos;

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

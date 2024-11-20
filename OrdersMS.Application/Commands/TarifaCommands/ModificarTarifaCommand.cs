using MediatR;
using OrdersMS.Application.Dtos.TarifaDtos;

namespace OrdersMS.Application.Commands.TarifaCommands
{
    public class ModificarTarifaCommand:IRequest
    {
        public ModificarTarifaCommand(ListarTarifaDto tarifaDto, Guid id)
        {
            TarifaDto = tarifaDto;
            Id = id;
        }

        public ListarTarifaDto TarifaDto { get; private set; }
       public  Guid Id {  get;  private set; }
    }
}

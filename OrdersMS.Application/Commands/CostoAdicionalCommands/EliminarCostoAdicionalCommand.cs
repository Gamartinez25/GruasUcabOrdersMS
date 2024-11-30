using MediatR;


namespace OrdersMS.Application.Commands.CostoAdicionalCommands
{
    public class EliminarCostoAdicionalCommand:IRequest
    {
        public EliminarCostoAdicionalCommand(Guid id)
        {
            Id= id;
        }

        public Guid Id { get; private set; }
    }
}

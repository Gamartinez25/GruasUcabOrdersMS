using MediatR;

namespace OrdersMS.Application.Commands.TarifaCommands
{
    public class EliminarTarifaCommand : IRequest
    {
        public EliminarTarifaCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

    }
}

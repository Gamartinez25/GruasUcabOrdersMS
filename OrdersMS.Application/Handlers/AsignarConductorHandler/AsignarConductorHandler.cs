
using MediatR;
using OrdersMS.Application.Commands.AsignarConductorCommand;

namespace OrdersMS.Application.Handlers.AsignarConductorHandler
{
    public class AsignarConductorHandler : IRequestHandler<AsignarConductorCommand>
    {
        public Task Handle(AsignarConductorCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Gracias Dios!!!!!!");
            return Task.CompletedTask;
        }
    }
}

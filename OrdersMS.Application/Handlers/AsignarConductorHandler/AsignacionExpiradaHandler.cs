
using MassTransit;
using MediatR;
using OrdersMS.Application.Commands.AsignarConductorCommand;
using OrdersMS.Application.Saga.Events;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Handlers.AsignarConductorHandler
{
    public class AsignacionExpiradaHandler:IRequestHandler<AsignacionExpiradaCommand>
    {
        private readonly IOrdenRepository OrdenRepository;
        private readonly IPublishEndpoint PublishEndpoint;

        public AsignacionExpiradaHandler(IOrdenRepository ordenRepository, IPublishEndpoint publishEndpoint)
        {
            OrdenRepository= ordenRepository;
            PublishEndpoint= publishEndpoint;
        }

        public async Task Handle(AsignacionExpiradaCommand request, CancellationToken cancellationToken)
        {
            var ordenes=await OrdenRepository.GetAllEstadoOrden();
            var ordenesPorAceptar = ordenes.Where(x => x.EstadoActual == "PorAceptar");
           
            foreach (var orden in ordenesPorAceptar)
            {
                TimeSpan? diferenciaEntreTiempos = DateTime.UtcNow - orden.UltimaActualizacion;
                double? tiempoTranscurridoMinutos = diferenciaEntreTiempos?.TotalMinutes;

                if (tiempoTranscurridoMinutos > 6)
                {
                    var evento = new ReasignarOrdenEvent(orden.CorrelationId);
                    await PublishEndpoint.Publish(evento, cancellationToken);
                }
            }
           
        }
    }
}

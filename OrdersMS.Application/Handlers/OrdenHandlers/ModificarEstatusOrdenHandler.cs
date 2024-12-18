
using FluentValidation;
using MassTransit;
using MediatR;
using OrdersMS.Application.Commands.OrdenCommands;
using OrdersMS.Application.Exceptions;
using OrdersMS.Application.Saga.Events;
using OrdersMS.Application.Validators.OrdenValidators;

namespace OrdersMS.Application.Handlers.OrdenHandlers
{
    public class ModificarEstatusOrdenHandler : IRequestHandler<ModificarEstatusOrdenCommand>
    {
        private readonly IPublishEndpoint PublishEndpoint;

        public ModificarEstatusOrdenHandler(IPublishEndpoint publishEndpoint)
        {
           PublishEndpoint = publishEndpoint;
        }

        public async  Task Handle(ModificarEstatusOrdenCommand request, CancellationToken cancellationToken)
        {
            var validator = new ModificarEstatusValidator();
            validator.ValidateAndThrow(request.EstatusDto);
           
                if (request.EstatusDto.TipoActualizacion == "Actualizar")
                {
                    var evento = new ActualizarOrdenEvent(request.EstatusDto.Id);
                    await PublishEndpoint.Publish(evento, cancellationToken);

                }
                if (request.EstatusDto.TipoActualizacion == "Cancelar")
                {
                    var evento = new OrdenCanceladaEvent(request.EstatusDto.Id);
                    await PublishEndpoint.Publish(evento, cancellationToken);
                }
                if (request.EstatusDto.TipoActualizacion == "Reasignar")
                {
                    var evento = new ReasignarOrdenEvent(request.EstatusDto.Id);
                    await PublishEndpoint.Publish(evento, cancellationToken);
                }




        }
    }
}

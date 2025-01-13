using AutoMapper;
using FluentValidation;
using MassTransit;
using MediatR;
using OrdersMS.Application.Commands.OrdenCommands;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Exceptions;
using OrdersMS.Application.Saga.Events;
using OrdersMS.Application.Validators.OrdenValidators;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Handlers.OrdenHandlers
{
    public class CrearOrdenHandler:IRequestHandler<CrearOrdenCommand>
    {
        private readonly IMapper Mapper;
        private readonly IOrdenRepository OrdenRepository;
        private readonly IPublishEndpoint PublishEndpoint;



        public CrearOrdenHandler(IMapper mapper, IOrdenRepository ordenRepository, IPublishEndpoint publishEndpoint)
        {
            Mapper = mapper;
            OrdenRepository = ordenRepository;
            PublishEndpoint = publishEndpoint;

        }

        public async Task Handle(CrearOrdenCommand request, CancellationToken cancellationToken)
        {
            ValidarOrden(request.CrearOrdenDto);
            var orden = MapperOrden(request.CrearOrdenDto);
            await OrdenRepository.AddOrdenAsync(orden);
            var evento = new OrdenCreadaEvent { OrdenId = orden.Id };
            await PublishEndpoint.Publish(evento, cancellationToken);

        }
        private  void ValidarOrden(CrearOrdenDto ordenDto)
        {
            var validator =new CrearOrdenValidator();
            validator.ValidateAndThrow(ordenDto);
            if (ordenDto.Administrador == null && ordenDto.Operador==null)
            {
                throw new InvalidIdException("El id del responsable de crear la orden no puede ser nulo");
            }
            if (ordenDto.Administrador != null && ordenDto.Operador != null)
            {
                throw new InvalidIdException("El responsable del registro de la orden es único");
            }
        }
        private OrdenDeServicio MapperOrden(CrearOrdenDto ordenDto)
        {
           var orden = Mapper.Map<OrdenDeServicio>(ordenDto);
           return orden;
        }
    }
}

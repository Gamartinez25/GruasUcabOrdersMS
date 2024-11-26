using AutoMapper;
using FluentValidation;
using MediatR;
using OrdersMS.Application.Commands.OrdenCommands;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Exceptions;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Handlers.OrdenHandlers
{
    public class CrearOrdenHandler:IRequestHandler<CrearOrdenCommand>
    {
        private readonly IMapper Mapper;
        private readonly IValidator<CrearOrdenDto> Validator;
        private readonly IOrdenRepository OrdenRepository;

        public CrearOrdenHandler(IMapper mapper, IOrdenRepository ordenRepository, IValidator<CrearOrdenDto> validator)
        {
            Mapper = mapper;
            OrdenRepository = ordenRepository;
            Validator = validator;
        }

        public async Task Handle(CrearOrdenCommand request, CancellationToken cancellationToken)
        {
            ValidarOrden(request.CrearOrdenDto);
            var orden = MapperOrden(request.CrearOrdenDto);
            await OrdenRepository.AddOrdenAsync(orden);
        }
        private  void ValidarOrden(CrearOrdenDto ordenDto)
        {
            Validator.ValidateAndThrow(ordenDto);
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

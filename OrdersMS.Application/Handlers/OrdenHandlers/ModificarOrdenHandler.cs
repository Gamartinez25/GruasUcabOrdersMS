using FluentValidation;
using MediatR;
using OrdersMS.Application.Commands.OrdenCommands;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Exceptions;
using OrdersMS.Application.Mappers.OrdenMappers;
using OrdersMS.Core.Repositories;

namespace OrdersMS.Application.Handlers.OrdenHandlers
{
    public class ModificarOrdenHandler : IRequestHandler<ModificarOrdenCommand>
    {
        private readonly IOrdenMapper OrdenMapper;
        private readonly IValidator<ModificarOrdenDto> Validator;
        private readonly IOrdenRepository OrdenRepository;
        public ModificarOrdenHandler(IOrdenMapper mapper, IOrdenRepository ordenRepository, IValidator<ModificarOrdenDto> validator)
        {
            OrdenMapper = mapper;
            OrdenRepository = ordenRepository;
            Validator = validator;
        }
        public  async Task Handle(ModificarOrdenCommand request, CancellationToken cancellationToken)
        {
          await ValidarOrden(request);
          var orden = await OrdenRepository.GetOrdenDeServicioByIdAsync(request.Id);
          var ordenModificada=OrdenMapper.ModificarOrden(orden, request.OrdenDto);
          await OrdenRepository.UpdateOrdenAsync(ordenModificada);
        }
        private async Task ValidarOrden(ModificarOrdenCommand request)
        { 
           
            Validator.ValidateAndThrow(request.OrdenDto);
            if (!request.Id.Equals(request.OrdenDto.Id))
            { throw new IdsOrdenNoCoincidenException($"Error al procesar la orden con ID {request.Id}. Los IDs de la solicitud y del DTO de la orden ({request.OrdenDto.Id}) deben coincidir. Verifica los datos de entrada."); }
            var ordenExistente= await OrdenRepository.GetOrdenDeServicioByIdAsync(request.OrdenDto.Id);
            if (ordenExistente == null)
            {
                throw new NotFoundOrderException("Orden no encontrada");
            }

        }
        

    }
}

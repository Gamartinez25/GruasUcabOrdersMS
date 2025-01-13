using AutoMapper;
using FluentValidation;
using MediatR;
using OrdersMS.Application.Commands.TarifaCommands;
using OrdersMS.Application.Dtos.TarifaDtos;
using OrdersMS.Application.Exceptions;
using OrdersMS.Application.Validators.TarifaValidators;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Handlers.TarifaHandlers
{
    public class ModificarTarifaHandler : IRequestHandler<ModificarTarifaCommand>
    {
        private readonly IMapper Mapper;
        private readonly ITarifaRepository TarifaRepository;

        public ModificarTarifaHandler(IMapper mapper, ITarifaRepository tarifaRepository)
        {
            Mapper = mapper;
            TarifaRepository = tarifaRepository;
        }
        public async Task Handle(ModificarTarifaCommand request, CancellationToken cancellationToken)
        {
            ValidarTarifa(request);
           var tarifa= MappearTarifa(request.TarifaDto);
            await TarifaRepository.UptadeTarifaAsync(tarifa);
           
            
        }
        private void ValidarTarifa(ModificarTarifaCommand request)
        {
            if (request.Id == Guid.Empty|| request.Id!=request.TarifaDto.Id) 
            {
                throw new InvalidIdException("El Id proporcionado es invalido");
            }
            var validator=new ModificarTarifaValidator();
            validator.ValidateAndThrow(request.TarifaDto);

        }
        private Tarifa MappearTarifa(ListarTarifaDto tarifaDto) 
        {
            var tarifa = Mapper.Map<Tarifa>(tarifaDto);
            return tarifa;
        }
    }
}

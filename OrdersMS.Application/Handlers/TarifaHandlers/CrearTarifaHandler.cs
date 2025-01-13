using AutoMapper;
using FluentValidation;
using MediatR;
using OrdersMS.Application.Commands.TarifaCommands;
using OrdersMS.Application.Dtos.TarifaDtos;
using OrdersMS.Application.Validators.TarifaValidators;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Handlers.TarifaHandlers
{
    public class CrearTarifaHandler : IRequestHandler<CrearTarifaCommand>
    {
        private readonly IMapper Mapper;
        private readonly ITarifaRepository TarifaRepository;

        public CrearTarifaHandler(IMapper mapper, ITarifaRepository tarifaRepository)
        {
            Mapper = mapper;
            TarifaRepository= tarifaRepository;
        }

        public async Task Handle(CrearTarifaCommand request, CancellationToken cancellationToken)
        {
            ValidarTarifa(request.CrearTarifaDto);
            var tarifa=MapperTarifa(request.CrearTarifaDto);
            await TarifaRepository.AddTarifaAsync(tarifa);
        }
        private  void ValidarTarifa(CrearTarifaDto tarifaDto)
        {
            var validator = new CrearTarifaValidator();
            validator.ValidateAndThrow(tarifaDto);

        }
        private Tarifa MapperTarifa(CrearTarifaDto tarifaDto) 
        {
          var tarifa=  Mapper.Map<Tarifa>(tarifaDto);
            return tarifa;
        }
    }
}

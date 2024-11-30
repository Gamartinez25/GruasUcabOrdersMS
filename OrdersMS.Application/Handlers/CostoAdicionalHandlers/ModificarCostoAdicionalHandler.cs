using AutoMapper;
using FluentValidation;
using MediatR;
using OrdersMS.Application.Commands.CostoAdicionalCommands;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;
using OrdersMS.Application.Exceptions;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Handlers.CostoAdicionalHandlers
{
    public class ModificarCostoAdicionalHandler : IRequestHandler<ModificarCostoAdicionalCommand>
    {
        private readonly IMapper Mapper;
        private readonly IValidator<ModificarCostoAdicionalDto> Validator;
        private readonly ICostoAdicionalRepository CostoAdicionalRepository;

        public ModificarCostoAdicionalHandler(IMapper mapper, ICostoAdicionalRepository costoAdicionalRepository, IValidator<ModificarCostoAdicionalDto> validator)
        {
            Mapper = mapper;
            CostoAdicionalRepository = costoAdicionalRepository;
            Validator = validator;
        }
        public async Task Handle(ModificarCostoAdicionalCommand request, CancellationToken cancellationToken)
        {
            // Validator.ValidateAndThrow(request.CostoAdicionalDto);
            await ValidarCostoAdicional(request);
            var existingCostoAdicional = await CostoAdicionalRepository.GetCostoAdicionalByIdAsync(request.Id);
            var nuevoCostoAdicional = Mapper.Map<OrdenCostoAdicional>((existingCostoAdicional, request.CostoAdicionalDto));
            await CostoAdicionalRepository.UpdateCostoAdicional(nuevoCostoAdicional);
        }
        private async Task ValidarCostoAdicional(ModificarCostoAdicionalCommand request)
        {
            Validator.ValidateAndThrow(request.CostoAdicionalDto);
            if (request.Id != request.CostoAdicionalDto.Id) { throw new IdsCostoAdicionalNoCoincidenException("Los ids del dto y de la peticion no coinciden"); }
            var existingCostoAdicional = await CostoAdicionalRepository.GetCostoAdicionalByIdAsync(request.Id);
            if (existingCostoAdicional.Estatus!= "Por Abrobar") { throw new InvalidAdditionalCostStateException("No se puede modificar un costo adicional que no está en estado Por Aprobar"); }
        }
    }
}

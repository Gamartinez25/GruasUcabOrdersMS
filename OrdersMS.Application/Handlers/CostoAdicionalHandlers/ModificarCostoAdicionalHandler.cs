using AutoMapper;
using FluentValidation;
using MediatR;
using OrdersMS.Application.Commands.CostoAdicionalCommands;
using OrdersMS.Application.Exceptions;
using OrdersMS.Application.Validators.CostoAdicionalValidators;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Handlers.CostoAdicionalHandlers
{
    public class ModificarCostoAdicionalHandler : IRequestHandler<ModificarCostoAdicionalCommand>
    {
        private readonly IMapper Mapper;
        private readonly ICostoAdicionalRepository CostoAdicionalRepository;

        public ModificarCostoAdicionalHandler(IMapper mapper, ICostoAdicionalRepository costoAdicionalRepository)
        {
            Mapper = mapper;
            CostoAdicionalRepository = costoAdicionalRepository;
        }
        public async Task Handle(ModificarCostoAdicionalCommand request, CancellationToken cancellationToken)
        {
            await ValidarCostoAdicional(request);
            var existingCostoAdicional = await CostoAdicionalRepository.GetCostoAdicionalByIdAsync(request.Id);
            var nuevoCostoAdicional = Mapper.Map<OrdenCostoAdicional>((existingCostoAdicional, request.CostoAdicionalDto));
            await CostoAdicionalRepository.UpdateCostoAdicional(nuevoCostoAdicional);
        }
        private async Task ValidarCostoAdicional(ModificarCostoAdicionalCommand request)
        {
            var validator=new ModificarCostoAdicionalValidator();
            validator.ValidateAndThrow(request.CostoAdicionalDto);
            if (request.Id != request.CostoAdicionalDto.Id) { throw new IdsCostoAdicionalNoCoincidenException("Los ids del dto y de la peticion no coinciden"); }
            var existingCostoAdicional = await CostoAdicionalRepository.GetCostoAdicionalByIdAsync(request.Id);
            if (existingCostoAdicional.Estatus!= "Por Aprobar") { throw new InvalidAdditionalCostStateException("No se puede modificar un costo adicional que no está en estado Por Aprobar"); }
        }
    }
}

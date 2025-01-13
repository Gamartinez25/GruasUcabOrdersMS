using AutoMapper;
using FluentValidation;
using MediatR;
using OrdersMS.Application.Commands.CostoAdicionalCommands;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;
using OrdersMS.Application.Validators.CostoAdicionalValidators;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;


namespace OrdersMS.Application.Handlers.CostoAdicionalHandlers
{
    public class RegistrarCostoAdicionalHandler : IRequestHandler<RegistrarCostoAdicionalCommand>
    {
        private readonly IMapper Mapper;
        private readonly ICostoAdicionalRepository CostoAdicionalRepository;

        public RegistrarCostoAdicionalHandler(IMapper mapper, ICostoAdicionalRepository costoAdicionalRepository)
        {
            Mapper = mapper;
            CostoAdicionalRepository = costoAdicionalRepository;
        }
        public async Task Handle(RegistrarCostoAdicionalCommand request, CancellationToken cancellationToken)
        {
            var validator = new CostoAdicionalValidator();
            validator.ValidateAndThrow(request.CostoAdicionalDto);
            var costoAdicional = Mapper.Map<OrdenCostoAdicional>(request.CostoAdicionalDto);
            await CostoAdicionalRepository.AddCostoAdicionalAsync(costoAdicional);
        }
    }
}

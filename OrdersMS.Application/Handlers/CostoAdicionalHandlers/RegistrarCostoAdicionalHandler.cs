using AutoMapper;
using FluentValidation;
using MediatR;
using OrdersMS.Application.Commands.CostoAdicionalCommands;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;


namespace OrdersMS.Application.Handlers.CostoAdicionalHandlers
{
    public class RegistrarCostoAdicionalHandler : IRequestHandler<RegistrarCostoAdicionalCommand>
    {
        private readonly IMapper Mapper;
        private readonly IValidator<RegistrarCostoAdicionalDto> Validator;
        private readonly ICostoAdicionalRepository CostoAdicionalRepository;

        public RegistrarCostoAdicionalHandler(IMapper mapper, ICostoAdicionalRepository costoAdicionalRepository, IValidator<RegistrarCostoAdicionalDto> validator)
        {
            Mapper = mapper;
            CostoAdicionalRepository = costoAdicionalRepository;
            Validator = validator;
        }
        public async Task Handle(RegistrarCostoAdicionalCommand request, CancellationToken cancellationToken)
        {
            Validator.ValidateAndThrow(request.CostoAdicionalDto);
            var costoAdicional = Mapper.Map<OrdenCostoAdicional>(request.CostoAdicionalDto);
            await CostoAdicionalRepository.AddCostoAdicionalAsync(costoAdicional);
        }
    }
}

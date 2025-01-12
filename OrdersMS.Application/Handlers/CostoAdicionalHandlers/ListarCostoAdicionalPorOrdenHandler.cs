using AutoMapper;
using FluentValidation;
using MediatR;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;
using OrdersMS.Application.Exceptions;
using OrdersMS.Application.Mappers.CostoAdicionalMappers;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;


namespace OrdersMS.Application.Handlers.CostoAdicionalHandlers
{
    public class ListarCostoAdicionalPorOrdenHandler : IRequestHandler<ListarCostoAdicionalPorOrdenQuery, IEnumerable<ListarCostosAdicionalesPorOrdenDto>>
    {
        private readonly ISalidaCostoAdicionalMapper Mapper;
        private readonly ICostoAdicionalRepository CostoAdicionalRepository;
        public ListarCostoAdicionalPorOrdenHandler(ISalidaCostoAdicionalMapper mapper, ICostoAdicionalRepository costoAdicionalRepository)
        {
            Mapper = mapper;
            CostoAdicionalRepository = costoAdicionalRepository;
        }
        public async Task<IEnumerable<ListarCostosAdicionalesPorOrdenDto>> Handle(ListarCostoAdicionalPorOrdenQuery request, CancellationToken cancellationToken)
        {

            var costosOrden = await CostoAdicionalRepository.GetAllCostoAdicionalAsync(request.Id);
            var nombresCostosAdicionales = await CostoAdicionalRepository.GetAllNombresCostosAdicionalesByIdAsync(request.Id);
            var ordenCostosDto= Mapper.Map(costosOrden, nombresCostosAdicionales);
            return ordenCostosDto;
        }
        
    }
}

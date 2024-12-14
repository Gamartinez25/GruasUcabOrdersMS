
using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Mappers.OrdenMappers;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;


namespace OrdersMS.Application.Handlers.OrdenHandlers
{
    public class ListarOrdenesHandler : IRequestHandler<ListarOrdenesQuery, IEnumerable<ListarOrdenesDto>>
    {
        private readonly IOrdenRepository OrdenRepository;
        private readonly ITarifaRepository TarifaRepository;
        private readonly IOrdenMapper OrdenMapper;
        public ListarOrdenesHandler(IOrdenMapper mapper, IOrdenRepository ordenRepository, ITarifaRepository tarifaRepository)
        {
            OrdenMapper = mapper;
            OrdenRepository = ordenRepository;
            TarifaRepository = tarifaRepository;

        }
        public async Task<IEnumerable<ListarOrdenesDto>> Handle(ListarOrdenesQuery request, CancellationToken cancellationToken)
        {
            var ordenes = await OrdenRepository.GetAllOrdenAsync();
            var estatusOrdenes = await OrdenRepository.GetAllEstadoOrden();
            var polizaAsegurados= await OrdenRepository.GetAllPolizaAseguradoAsync();
            var asegurados = await OrdenRepository.GetAllAseguradoAsync();
            var polizas= await OrdenRepository.GetAllPolizaAsync();
            var tarifas= await TarifaRepository.GetAllTarifaAsync();
            return OrdenMapper.ListarOrdenesDtos(ordenes, estatusOrdenes, polizaAsegurados,polizas,asegurados,tarifas);
           
        }

    }
}

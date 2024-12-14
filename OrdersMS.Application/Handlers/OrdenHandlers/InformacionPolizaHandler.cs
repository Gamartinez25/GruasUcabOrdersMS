
using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Mappers.OrdenMappers;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;

namespace OrdersMS.Application.Handlers.OrdenHandlers
{
    public class InformacionPolizaHandler:IRequestHandler<InformacionPolizaQuery,InformacionPolizaDto>
    {
        private readonly IOrdenRepository OrdenRepository;
        private readonly ITarifaRepository TarifaRepository;
        private readonly IOrdenMapper OrdenMapper;
        public InformacionPolizaHandler(IOrdenMapper mapper, IOrdenRepository ordenRepository, ITarifaRepository tarifaRepository)
        {
            OrdenMapper = mapper;
            OrdenRepository = ordenRepository;
            TarifaRepository = tarifaRepository;

        }

        public async  Task<InformacionPolizaDto> Handle(InformacionPolizaQuery request, CancellationToken cancellationToken)
        { var polizaAsegurado = await OrdenRepository.GetPolizaAseguradoById(request.Id);
            if (polizaAsegurado != null && polizaAsegurado.Estatus == "Activo") 
            {
                var polizaAsegurados = await OrdenRepository.GetAllPolizaAseguradoAsync();
                var asegurados = await OrdenRepository.GetAllAseguradoAsync();
                var polizas = await OrdenRepository.GetAllPolizaAsync();
                var tarifas = await TarifaRepository.GetAllTarifaAsync();
                var informacion = OrdenMapper.ConsultarInformacionPoliza(request.Id, polizaAsegurados, polizas, asegurados, tarifas);
                return informacion;
            }
            throw new InvalidOperationException("La póliza asegurada debe estar activa para realizar esta operación");
        }
    }
}

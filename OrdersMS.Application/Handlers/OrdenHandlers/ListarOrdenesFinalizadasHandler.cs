using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;
using OrdersMS.Core.Services.IGoogleServices;
using OrdersMS.Domain.Services;

namespace OrdersMS.Application.Handlers.OrdenHandlers
{
    public class ListarOrdenesFinalizadasHandler : IRequestHandler<ListarOrdenesFinalizadasQuery, List<OrdenesFinalizadasDto>>
    {
        private readonly IOrdenRepository OrdenRepository;
        private readonly IGoogleService GoogleService;
        
        public ListarOrdenesFinalizadasHandler(IOrdenRepository ordenRepository, IGoogleService googleService)
        {
            OrdenRepository = ordenRepository;
            GoogleService = googleService;
        }

        public async Task<List<OrdenesFinalizadasDto>> Handle(ListarOrdenesFinalizadasQuery request, CancellationToken cancellationToken)
        {
            var ordenes = await OrdenRepository.GetAllOrdenAsync();
            var ordenesPorGruero = ordenes.Where(x => x.Vehiculo == request.IdVehiculo);
            List<OrdenesFinalizadasDto> ordenesFinalizadas = new List<OrdenesFinalizadasDto>();
            foreach (var orden in ordenesPorGruero)
            {
                var estatus = await OrdenRepository.GetEstadoOrdenByIdOrdenAsync(orden.Id);
                var polizaAsegurado = await OrdenRepository.GetPolizaAseguradoById(orden.PolizaAseguradoId);
                if (estatus.EstadoActual == "Finalizado" || estatus.EstadoActual == "Pagado")
                {
                    var (latOrigen, lonOrigen) = ObtenerCoordenadas.SepararCoordenadas(orden.DireccionOrigen);
                    var (latDestino, lonDestino) = ObtenerCoordenadas.SepararCoordenadas(orden.DireccionDestino);
                    var direccionOrigen = await GoogleService.GetDirecction(latOrigen, lonOrigen);
                    var direccionDestino = await GoogleService.GetDirecction(latDestino, lonDestino);
                    var ordenFinalizada = new OrdenesFinalizadasDto(orden.Id,
                                                                        orden.NumeroFactura,
                                                                        orden.NombreDenunciante,
                                                                        orden.Fecha.ToString(),
                                                                        direccionOrigen,
                                                                        direccionDestino,
                                                                        "#" + polizaAsegurado.Placa + ", " + polizaAsegurado.Marca + " " + polizaAsegurado.Modelo + ", " + polizaAsegurado.Color + ", " + polizaAsegurado.Anio + "."
                                                                        , orden.DetallesIncidente, orden.CostoTotal, orden.CostoServiciosAdicionales.Value, estatus.EstadoActual);

                    ordenesFinalizadas.Add(ordenFinalizada);
                }

            }
            return ordenesFinalizadas;
        }
    }
}

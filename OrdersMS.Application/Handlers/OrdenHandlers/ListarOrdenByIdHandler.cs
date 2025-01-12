using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;
using OrdersMS.Core.Services.IGoogleServices;
using OrdersMS.Domain.Services;

namespace OrdersMS.Application.Handlers.OrdenHandlers
{
    public class ListarOrdenByIdHandler : IRequestHandler<ListarOrdenByIdQuery, OrdenByIdDto>
    {
        private readonly IOrdenRepository OrdenRepository;
        private readonly IGoogleService GoogleService;
        
        public ListarOrdenByIdHandler(IOrdenRepository ordenRepository, IGoogleService googleService)
        {
            OrdenRepository = ordenRepository;
            GoogleService = googleService;
        }

        public async Task<OrdenByIdDto> Handle(ListarOrdenByIdQuery request, CancellationToken cancellationToken)
        {
            var ordenes = await OrdenRepository.GetAllOrdenAsync();
            var orden = ordenes.Where(x => x.Id == request.IdOrden).First();
            OrdenByIdDto ordenFinalizada;
           
                var estatus = await OrdenRepository.GetEstadoOrdenByIdOrdenAsync(orden.Id);
                var polizaAsegurado = await OrdenRepository.GetPolizaAseguradoById(orden.PolizaAseguradoId);
                    var (latOrigen, lonOrigen) = ObtenerCoordenadas.SepararCoordenadas(orden.DireccionOrigen);
                    var (latDestino, lonDestino) = ObtenerCoordenadas.SepararCoordenadas(orden.DireccionDestino);
                    var direccionOrigen = await GoogleService.GetDirecction(latOrigen, lonOrigen);
                    var direccionDestino = await GoogleService.GetDirecction(latDestino, lonDestino);
                    ordenFinalizada = new OrdenByIdDto(orden.Id,
                                                                        orden.NumeroFactura,
                                                                        orden.NombreDenunciante,
                                                                        
                                                                        orden.Fecha.ToString(),
                                                                        direccionOrigen,
                                                                        direccionDestino,
                                                                        "#" + polizaAsegurado.Placa + ", " + polizaAsegurado.Marca + " " + polizaAsegurado.Modelo + ", " + polizaAsegurado.Color + ", " + polizaAsegurado.Anio + "."
                                                                        , orden.DetallesIncidente, orden.CostoTotal, orden.CostoServiciosAdicionales.Value, estatus.EstadoActual);

                return ordenFinalizada;   
        }
    }
}

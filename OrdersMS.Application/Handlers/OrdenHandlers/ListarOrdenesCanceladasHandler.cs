using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;
using OrdersMS.Core.Services.IGoogleServices;
using OrdersMS.Domain.Services;

namespace OrdersMS.Application.Handlers.OrdenHandlers
{
    public class ListarOrdenesCanceladasHandler : IRequestHandler<ListarOrdenesCanceladasQuery, List<OrdenCanceladaDto>>
    {
        private readonly IOrdenRepository OrdenRepository;
        private readonly IGoogleService GoogleService;

        public ListarOrdenesCanceladasHandler(IOrdenRepository ordenRepository, IGoogleService googleService)
        {
            OrdenRepository = ordenRepository;
            GoogleService = googleService;
        }
        public  async Task<List<OrdenCanceladaDto>> Handle(ListarOrdenesCanceladasQuery request, CancellationToken cancellationToken)
        {
            var ordenes = await OrdenRepository.GetAllOrdenAsync();
            var ordenesPorGruero = ordenes.Where(x => x.Vehiculo == request.IdVehiculo);
            List<OrdenCanceladaDto> ordenesCanceladas = new List<OrdenCanceladaDto>();
            foreach (var orden in ordenesPorGruero)
            {
                var estatus = await OrdenRepository.GetEstadoOrdenByIdOrdenAsync(orden.Id);
                if (estatus.EstadoActual == "Cancelado")
                {
                    var (latOrigen, lonOrigen) = ObtenerCoordenadas.SepararCoordenadas(orden.DireccionOrigen);
                    var (latDestino, lonDestino) = ObtenerCoordenadas.SepararCoordenadas(orden.DireccionDestino);
                    var direccionOrigen = await GoogleService.GetDirecction(latOrigen, lonOrigen);
                    var direccionDestino = await GoogleService.GetDirecction(latDestino, lonDestino);
                    var ordenCancelada= new OrdenCanceladaDto(orden.Id,
                                                              orden.NombreDenunciante,
                                                              direccionOrigen,
                                                              direccionDestino,
                                                              orden.Fecha.ToString(),
                                                              estatus.EstadoActual);
                    ordenesCanceladas.Add(ordenCancelada);
                }

            }
            return ordenesCanceladas;
        }
    }
}

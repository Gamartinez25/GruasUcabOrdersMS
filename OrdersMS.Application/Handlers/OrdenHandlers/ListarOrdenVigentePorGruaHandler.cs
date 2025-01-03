using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;
using OrdersMS.Core.Services.IGoogleServices;
using OrdersMS.Domain.Services;

namespace OrdersMS.Application.Handlers.OrdenHandlers
{
    public class ListarOrdenVigentePorGruaHandler : IRequestHandler<ListarOrdenVigentePorGruaQuery, OrdenVigentePorGruaDto>
    {
        private readonly IOrdenRepository OrdenRepository;
        private readonly IGoogleService GoogleService;

        public ListarOrdenVigentePorGruaHandler(IOrdenRepository ordenRepository, IGoogleService googleService)
        {
            OrdenRepository = ordenRepository;
            GoogleService = googleService;
        }

        public async Task<OrdenVigentePorGruaDto> Handle(ListarOrdenVigentePorGruaQuery request, CancellationToken cancellationToken)
        {
            var ordenes = await OrdenRepository.GetAllOrdenAsync();
            var ordenesPorGruero=ordenes.Where(x=>x.Vehiculo==request.IdGrua);
            OrdenVigentePorGruaDto ordenVigente = null;
            foreach (var orden in ordenesPorGruero)
            {
                var estatus=await OrdenRepository.GetEstadoOrdenByIdOrdenAsync(orden.Id);
                if (estatus.EstadoActual != "PorAsignar" && estatus.EstadoActual != "Cancelado" && estatus.EstadoActual != "Finalizado" && estatus.EstadoActual != "Pagado")    
                {
                    var (latOrigen, lonOrigen) = ObtenerCoordenadas.SepararCoordenadas(orden.DireccionOrigen);
                    var direccionOrigen = await GoogleService.GetDirecction(latOrigen, lonOrigen);   
                    var (latDestino, lonDestino)= ObtenerCoordenadas.SepararCoordenadas(orden.DireccionDestino);
                    
                    var direccionDestino = await GoogleService.GetDirecction(latDestino, lonDestino);
                    ordenVigente = new OrdenVigentePorGruaDto(orden.Id,
                                                                   orden.DetallesIncidente,
                                                                   direccionOrigen,
                                                                   direccionDestino,
                                                                   orden.NombreDenunciante,
                                                                   estatus.EstadoActual);
                    break;
                
                }
            }

            return ordenVigente;
        }
    }
}

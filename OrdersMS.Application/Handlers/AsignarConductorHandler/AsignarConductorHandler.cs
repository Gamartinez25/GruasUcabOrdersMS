using MassTransit;
using MediatR;
using OrdersMS.Application.Commands.AsignarConductorCommand;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Exceptions;
using OrdersMS.Application.Mappers.OrdenMappers;
using OrdersMS.Application.Saga.Events;
using OrdersMS.Application.Services;
using OrdersMS.Core.Repositories;
using OrdersMS.Core.Services.IGoogleServices;
using OrdersMS.Core.Services.MsUsers;
using OrdersMS.Domain.Entities;
using OrdersMS.Domain.Services;
using OrdersMS.Infrastructure.Mappers;

namespace OrdersMS.Application.Handlers.AsignarConductorHandler
{
    public class AsignarConductorHandler : IRequestHandler<AsignarConductorCommand, VehiculosDisponiblesAsignarOrdenDto>
    {
        private readonly IOrdenRepository OrdenRepository;
        private readonly IGoogleService GoogleService;
        private readonly IOrdenMapper OrdenMapper;
        private readonly IPublishEndpoint PublishEndpoint;
        private readonly IVehiculosAsignadosRepository VehiculosAsignadosRepository;
        private readonly IUserMsService UserMsService;
        
        public AsignarConductorHandler(IOrdenRepository ordenRepository, IGoogleService googleService,IOrdenMapper ordenMapper, IPublishEndpoint publishEndpoint,IVehiculosAsignadosRepository vehiculosAsignados, IUserMsService userMsService)
        {
            OrdenRepository = ordenRepository;
            GoogleService = googleService;
            OrdenMapper = ordenMapper;
            PublishEndpoint = publishEndpoint;
            VehiculosAsignadosRepository = vehiculosAsignados;
            UserMsService = userMsService;

        }

        public  async Task<VehiculosDisponiblesAsignarOrdenDto> Handle(AsignarConductorCommand request, CancellationToken cancellationToken)
        {
            // Obtención de datos iniciales
            var estatusOrdenes = await OrdenRepository.GetAllEstadoOrden();
            var orden = await OrdenRepository.GetOrdenDeServicioByIdAsync(request.OrdenId);
            var tarifa = await OrdenRepository.GetTarifaByIdOrdenAsync(request.OrdenId);

            //Validar si el estatus de la orden es el adecuado
            if (estatusOrdenes.First(x => x.CorrelationId == request.OrdenId).EstadoActual != "PorAsignar")
                throw new ValidatorException("Para Asignar un conductor a una orden este debe estar en un estdo de PorAsignar");

             // Cálculo de coordenadas
            var (latOrigen, lonOrigen) = ObtenerCoordenadas.SepararCoordenadas(orden.DireccionOrigen);
            var (latDestino, lonDestino) = ObtenerCoordenadas.SepararCoordenadas(orden.DireccionDestino);

            // Obtención de rutas
            var rutaOrigenDestino = await GoogleService.GetDistanceToOriginAccidentDestination(latOrigen, lonOrigen, latDestino, lonDestino);
            
            // Verificar disponibilidad de vehículos
            var gruaAsignada =await  AsignarGrua(request.OrdenId, orden,estatusOrdenes.ToList());
            if (gruaAsignada==null)
                throw new InvalidOperationException("No hay gruas disponibles para asignar.");
            // Cálculo de distancias
           
            double distanciaTotal = CalcularDistanciaTotal(gruaAsignada.DistanciaValor, rutaOrigenDestino.DistanciaValor);
            double cantidadKmExtra = CalcularKmExtra(tarifa.DistanciaKm, distanciaTotal);
            double costoKmExtra = cantidadKmExtra * tarifa.CostoPorKm;
            // Modificar orden
            var ordenDto = new ModificarOrdenDto(request.OrdenId, gruaAsignada.Id, cantidadKmExtra, costoKmExtra);
            var ordenModificada = OrdenMapper.ModificarOrden(tarifa, orden, ordenDto);
            //Guardar los cambios
            await OrdenRepository.UpdateOrdenAsync(ordenModificada);
            VehiculosAsignadosRepository.AddAsignacionGrua(request.OrdenId,gruaAsignada.Id);

              

            // Preparar resultado
            var vehiculoDto = CrearVehiculoAsignarOrdenDto(gruaAsignada, distanciaTotal, cantidadKmExtra, costoKmExtra);

            // Publicar evento
            var evento = new ActualizarOrdenEvent(request.OrdenId);
            await PublishEndpoint.Publish(evento, cancellationToken);
            await UserMsService.SendNotification(gruaAsignada.Id);

            return vehiculoDto;
        }
        private static double CalcularDistanciaTotal(double distanciaVehiculo, double distanciaRuta)
        {
            return Math.Round((distanciaVehiculo + distanciaRuta) / 1000, 1);
        }

        private static double CalcularKmExtra(double distanciaPermitida, double distanciaTotal)
        {
            double diferencia = distanciaPermitida - distanciaTotal;
            return Math.Round(diferencia > 0 ? 0 : Math.Abs(diferencia),2);
        }
        private static VehiculosDisponiblesAsignarOrdenDto CrearVehiculoAsignarOrdenDto(VehiculoDto vehiculo, double distanciaTotal, double cantidadKmExtra, double costoKmExtra)
        {
            return new VehiculosDisponiblesAsignarOrdenDto(
                vehiculo.Id,
                vehiculo.Tipo,
                vehiculo.Marca,
                vehiculo.Modelo,
                vehiculo.Placa,
                vehiculo.Color,
                vehiculo.DistanciaTexto,
                distanciaTotal.ToString(),
                vehiculo.DuracionTexto,
                cantidadKmExtra,
                costoKmExtra);
        }
        private async Task<VehiculoDto>  AsignarGrua(Guid IdOrden,OrdenDeServicio orden,List<EstadoOrden> estatusOrdenes)
        {
           
            var ordenes = await OrdenRepository.GetAllOrdenAsync();
            var gruasPreviamenteAsignadas = VehiculosAsignadosRepository.GetGruasAsignadasPreviamente(IdOrden);
            // Cálculo de coordenadas
            var (latOrigen, lonOrigen) = ObtenerCoordenadas.SepararCoordenadas(orden.DireccionOrigen);

            // Obtención de rutas
            var rutasVehiculoOrigen = await GoogleService.GetDistanceAvailableVehiclesToOrigin(latOrigen, lonOrigen);

            // Verificar disponibilidad de vehículos
            var listaVehiculosDisponibles = VerificarDisponibilidadVehiculo.validarDisponiblidadVehiculos(rutasVehiculoOrigen, estatusOrdenes.ToList(), ordenes.ToList());
            if (!listaVehiculosDisponibles.Any())
                throw new InvalidOperationException("No hay vehículos disponibles para asignar.");
            if (gruasPreviamenteAsignadas == null)
                return listaVehiculosDisponibles.First();
            else
             return listaVehiculosDisponibles.Where(vehiculo => !gruasPreviamenteAsignadas.Contains(vehiculo.Id)).ToList().First();

        }
    }
}

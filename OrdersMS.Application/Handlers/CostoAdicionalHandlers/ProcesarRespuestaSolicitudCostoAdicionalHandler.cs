using MediatR;
using OrdersMS.Application.Commands.CostoAdicionalCommands;
using OrdersMS.Application.Services;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Handlers.CostoAdicionalHandlers
{
    public class ProcesarRespuestaSolicitudCostoAdicionalHandler : IRequestHandler<ProcesarRespuestaSolicitudCostoAdicionalCommand>
    {
        private readonly ICostoAdicionalRepository CostoAdicionalRepository;
        private readonly IOrdenRepository OrdenRepository;

        public ProcesarRespuestaSolicitudCostoAdicionalHandler(ICostoAdicionalRepository costoAdicionalRepository, IOrdenRepository ordenRepository)
        {
            CostoAdicionalRepository = costoAdicionalRepository;
            OrdenRepository = ordenRepository;
            
        }

        public async Task Handle(ProcesarRespuestaSolicitudCostoAdicionalCommand request, CancellationToken cancellationToken)
        { 
            
            var costo=await  CostoAdicionalRepository.GetCostoAdicionalByIdAsync(request.IdCostAdicional);
            var orden = await OrdenRepository.GetOrdenDeServicioByIdAsync(costo.OrdenDeServicioId);
            await ValidarSolicitud(orden, request.RespuestaSolicitudCostAdicional);
                
            if (request.RespuestaSolicitudCostAdicional == "Aprobado")
            {
                costo.ActualizarEstatus("Aprobado");
                await CostoAdicionalRepository.UpdateCostoAdicional(costo);
                var costos= await CostoAdicionalRepository.GetAllCostoAdicionalAsync(orden.Id);
                var costosAprobados = costos.Where(x => x.Estatus == "Aprobado").ToList();
                double totalCostosAdicionales=0;
                foreach(var costoAprobado in costosAprobados)
                {
                   totalCostosAdicionales = totalCostosAdicionales+costoAprobado.Costo;
                }
                var tarifa = await OrdenRepository.GetTarifaByIdOrdenAsync(costo.OrdenDeServicioId);
                var total = CalcularTotalOrden.CalcularTotalOrdenDeServicio(tarifa, orden.CostoTotalKmExtra, totalCostosAdicionales);
                orden.ActualizarCostosServiciosAdicionales(totalCostosAdicionales);
                orden.ActualizarTotal(total);
                await OrdenRepository.UpdateOrdenAsync(orden);
            }
            else
            {
                costo.ActualizarEstatus("Rechazado");
                await CostoAdicionalRepository.UpdateCostoAdicional(costo);
            }
           
            
        }
        private async Task ValidarSolicitud(OrdenDeServicio orden, string tipoSolicitudEsperada)
        {
            if (tipoSolicitudEsperada != "Aprobado" && tipoSolicitudEsperada != "Rechazado")
                throw new InvalidOperationException("El tipo de solitud no es valida");
            var estadoOrden = await OrdenRepository.GetEstadoOrdenByIdOrdenAsync(orden.Id);
            if (estadoOrden.EstadoActual == "Finalizado" || estadoOrden.EstadoActual == "Pagado" || estadoOrden.EstadoActual == "Cancelado")
                throw new InvalidOperationException("No se pueden agregar costos adicionales a una orden finalizada");
        }
     

    }
}

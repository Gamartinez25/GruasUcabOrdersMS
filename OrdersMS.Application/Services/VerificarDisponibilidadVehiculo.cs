using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;
using OrdersMS.Infrastructure.Mappers;

namespace OrdersMS.Application.Services
{
    public static  class VerificarDisponibilidadVehiculo
    {
        public static  List<VehiculoDto> validarDisponiblidadVehiculos(List<VehiculoDto> rutasVehiculoOrigen,List<EstadoOrden> estatusOrdenes,List<OrdenDeServicio> ordenes)
        {
          
            var estadosNoDisponibles = new[] { "Aceptado", "Localizado", "EnProceso", "PorAceptar" };
            var idsOrdenesNoDisponibles = estatusOrdenes
                .Where(x => estadosNoDisponibles.Contains(x.EstadoActual))
                .Select(x => x.CorrelationId)
                .ToHashSet();

            var idsVehiculosNoDisponibles = ordenes
                .Where(x => idsOrdenesNoDisponibles.Contains(x.Id))
                .Select(x => x.Vehiculo)
                .ToHashSet();

            var vehiculosDisponibles = rutasVehiculoOrigen.Where(x => !idsVehiculosNoDisponibles.Contains(x.Id));
            return vehiculosDisponibles.ToList();
        }
    }
}

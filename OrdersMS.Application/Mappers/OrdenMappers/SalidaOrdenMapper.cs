using AutoMapper;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Mappers.OrdenMappers
{
    public class SalidaOrdenMapper : IOrdenMapper
    {
        public IEnumerable<ListarOrdenesDto> ListarOrdenesDtos(IEnumerable<OrdenDeServicio> ordenes, IEnumerable<PolizaAsegurado> polizaAsegurados, IEnumerable<Poliza> polizas, IEnumerable<Asegurado> asegurados, IEnumerable<Tarifa> tarifas)
        {
            var polizaAseguradosDiccionario = polizaAsegurados.ToDictionary(a => a.Id);
            var aseguradosDiccionario = asegurados.ToDictionary(a => a.Id);
            var polizasDiccionario = polizas.ToDictionary(a => a.Id);
            var tarifasDiccionario = tarifas.ToDictionary(a => a.Id);

            var ordenesDto = new List<ListarOrdenesDto>();
            foreach (var orden in ordenes)
            {
                var polizaAsegurado = polizaAseguradosDiccionario[orden.PolizaAseguradoId];
                var poliza = polizasDiccionario[polizaAsegurado.PolizaId];
                var tarifa = tarifasDiccionario[poliza.TarifaId];
                var asegurado = aseguradosDiccionario[polizaAsegurado.AseguradoId];
                var ordenDto = new ListarOrdenesDto
                (
                orden.Id,
                orden.DetallesIncidente,
                orden.Fecha,
                orden.DireccionOrigen,
                orden.DireccionDestino,
                orden.CantidadKmExtra,
                orden.Estatus,
                orden.NombreDenunciante,
                orden.TipoDocumentoDenunciante,
                orden.NumeroDocumentoDenunciante,
                orden.CostoTotalKmExtra,
                orden.CostoTotal,
                orden.CostoServiciosAdicionales,
                orden.CreadoPor,
                orden.Vehiculo,
                polizaAsegurado.Placa,
                polizaAsegurado.Marca,
                polizaAsegurado.Modelo,
                polizaAsegurado.Anio,
                polizaAsegurado.FechaInicioCobertura,
                polizaAsegurado.FechaVencimientoCobertura,
                polizaAsegurado.TipoVehiculo,
                polizaAsegurado.Color,
                tarifa.CostoBase,
                tarifa.DistanciaKm,
                 polizaAsegurado.Estatus,
                asegurado.Nombres + ' ' + asegurado.Apellidos,
                asegurado.FechaNacimiento,
                asegurado.TipoDocumento + '-' + asegurado.NumeroDocumento,
                asegurado.Estatus,
                orden.PolizaAseguradoId
                );
                ordenesDto.Add(ordenDto);
            }
            return ordenesDto;
        }
    }
}

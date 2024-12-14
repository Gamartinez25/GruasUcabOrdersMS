using AutoMapper;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Mappers.OrdenMappers
{
    public class OrdenMapper : IOrdenMapper
    {
        public InformacionPolizaDto ConsultarInformacionPoliza(Guid id, IEnumerable<PolizaAsegurado> polizaAsegurados, IEnumerable<Poliza> polizas, IEnumerable<Asegurado> asegurados, IEnumerable<Tarifa> tarifas)
        {
            var polizaAseguradosDiccionario = polizaAsegurados.ToDictionary(a => a.Id);
            var aseguradosDiccionario = asegurados.ToDictionary(a => a.Id);
            var polizasDiccionario = polizas.ToDictionary(a => a.Id);
            var tarifasDiccionario = tarifas.ToDictionary(a => a.Id);
            var polizaAsegurado = polizaAseguradosDiccionario[id];
            var poliza = polizasDiccionario[polizaAsegurado.PolizaId];
            var tarifa = tarifasDiccionario[poliza.TarifaId];
            var asegurado = aseguradosDiccionario[polizaAsegurado.AseguradoId];
            var informacion = new InformacionPolizaDto(
                id, asegurado.Nombres + ' ' + asegurado.Apellidos,
                asegurado.TipoDocumento + '-' + asegurado.NumeroDocumento,
                tarifa.CostoBase,
                tarifa.DistanciaKm,
                poliza.Nombre,
                polizaAsegurado.Placa,
                polizaAsegurado.Marca+ ", " +polizaAsegurado.Modelo + ", "+polizaAsegurado.Color);
            return informacion;
        }

        public IEnumerable<ListarOrdenesDto> ListarOrdenesDtos(IEnumerable<OrdenDeServicio> ordenes, IEnumerable<EstadoOrden> estatus, IEnumerable<PolizaAsegurado> polizaAsegurados, IEnumerable<Poliza> polizas, IEnumerable<Asegurado> asegurados, IEnumerable<Tarifa> tarifas)
        {
            var polizaAseguradosDiccionario = polizaAsegurados.ToDictionary(a => a.Id);
            var aseguradosDiccionario = asegurados.ToDictionary(a => a.Id);
            var polizasDiccionario = polizas.ToDictionary(a => a.Id);
            var tarifasDiccionario = tarifas.ToDictionary(a => a.Id);
            var estatusDiccionario = estatus.ToDictionary(a => a.CorrelationId);

            var ordenesDto = new List<ListarOrdenesDto>();
            foreach (var orden in ordenes)
            {
                var polizaAsegurado = polizaAseguradosDiccionario[orden.PolizaAseguradoId];
                var poliza = polizasDiccionario[polizaAsegurado.PolizaId];
                var tarifa = tarifasDiccionario[poliza.TarifaId];
                var asegurado = aseguradosDiccionario[polizaAsegurado.AseguradoId];
                var estatusOrden= estatusDiccionario[orden.Id];
                var ordenDto = new ListarOrdenesDto
                (
                orden.Id,
                orden.DetallesIncidente,
                orden.Fecha,
                orden.DireccionOrigen,
                orden.DireccionDestino,
                orden.CantidadKmExtra,
                estatusOrden.EstadoActual,
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

        public OrdenDeServicio ModificarOrden(OrdenDeServicio orden, ModificarOrdenDto ordenDto)
        {
            var nuevaOrden = new OrdenDeServicio(orden.Id, orden.Fecha, orden.DetallesIncidente, orden.DireccionOrigen,
                orden.DireccionDestino, ordenDto.CostoTotal, orden.NombreDenunciante, orden.TipoDocumentoDenunciante,
                orden.NumeroDocumentoDenunciante, orden.PolizaAseguradoId, orden.Administrador, orden.Operador,
                ordenDto.Vehiculo, ordenDto.CantidadKmExtra, orden.CostoServiciosAdicionales, ordenDto.CostoTotalKmExtra);
            return nuevaOrden;
        }
    }
}

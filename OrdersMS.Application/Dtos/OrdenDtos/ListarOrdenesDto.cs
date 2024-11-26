using OrdersMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrdersMS.Application.Dtos.OrdenDtos
{
    public class ListarOrdenesDto
    {
        public ListarOrdenesDto(Guid id,
                                string detallesIncidente,
                                DateTime fecha,
                                string direccionOrigen,
                                string direccionDestino,
                                double? cantidadKmExtra,
                                string estatus,
                                string nombreDenunciante,
                                string tipoDocumentoDenunciante,
                                string numeroDocumentoDenunciante,
                                double? costoTotalKmExtra,
                                double? costoTotal,
                                double? costoServiciosAdicionales,
                                string usuarioCreadorOrden,
                                Guid? idGruaAsignada,
                                string placaVehiculo,
                                string marca,
                                string modelo,
                                string anio,
                                string polizaFechaInicio,
                                string polizaFechaFin,
                                string tipoVehiculo,
                                string color,
                                double coberturaBasePoliza,
                                double distanciaCoberturaPoliza,
                                string estatusPolizaAsegurado,
                                string nombreAsegurado,
                                string fechaNacimiento,
                                string documentoIdentidad,
                                string estatusAsegurado,
                                Guid polizaAseguradoId)
        {
            Id = id;
            DetallesIncidente = detallesIncidente;
            Fecha = fecha;
            DireccionOrigen = direccionOrigen;
            DireccionDestino = direccionDestino;
            CantidadKmExtra = cantidadKmExtra;
            Estatus = estatus;
            NombreDenunciante = nombreDenunciante;
            TipoDocumentoDenunciante = tipoDocumentoDenunciante;
            NumeroDocumentoDenunciante = numeroDocumentoDenunciante;
            CostoTotalKmExtra = costoTotalKmExtra;
            CostoTotal = costoTotal;
            CostoServiciosAdicionales = costoServiciosAdicionales;
            UsuarioCreadorOrden = usuarioCreadorOrden;
            IdGruaAsignada = idGruaAsignada;
            PlacaVehiculo = placaVehiculo;
            Marca = marca;
            Modelo = modelo;
            Anio = anio;
            PolizaFechaInicio = polizaFechaInicio;
            PolizaFechaFin = polizaFechaFin;
            TipoVehiculo = tipoVehiculo;
            Color = color;
            CoberturaBasePoliza = coberturaBasePoliza;
            DistanciaCoberturaPoliza = distanciaCoberturaPoliza;
            EstatusPolizaAsegurado = estatusPolizaAsegurado;
            NombreAsegurado = nombreAsegurado;
            FechaNacimiento = fechaNacimiento;
            DocumentoIdentidad = documentoIdentidad;
            EstatusAsegurado = estatusAsegurado;
            PolizaAseguradoId = polizaAseguradoId;
        
    }

        public Guid Id { get; private set; }
        public string DetallesIncidente { get; private set; }
        public DateTime Fecha { get; private set; }
        public string DireccionOrigen { get; private set; }
        public string DireccionDestino { get; private set; }
        public double? CantidadKmExtra { get; private set; }
        public string Estatus { get; private set; }
        public string NombreDenunciante { get; private set; }
        public string TipoDocumentoDenunciante { get; private set; }
        public string NumeroDocumentoDenunciante { get; private set; }
        public double? CostoTotalKmExtra { get; private set; }
        public double? CostoTotal { get; private set; }
        public double? CostoServiciosAdicionales { get; private set; }
        public string UsuarioCreadorOrden { get; private set; }
        public Guid? IdGruaAsignada { get; private set; }
        public string PlacaVehiculo { get; private set; }
        public string Marca {  get; private set; }
        public string Modelo { get; private set; }
        public string Anio { get; private set; }
        public string PolizaFechaInicio { get; private set; }
        public string PolizaFechaFin { get; private set; }
        public string TipoVehiculo { get; private set; }
        public string Color { get; private set; }
        public double CoberturaBasePoliza { get; private set; } 
        public double DistanciaCoberturaPoliza { get; private set; }    
        public string EstatusPolizaAsegurado { get; private set; }  
        public string NombreAsegurado { get; private set; }
        public string FechaNacimiento { get; private set; }
        public string DocumentoIdentidad { get; private set; }
        public string EstatusAsegurado { get; private set; }
        public Guid PolizaAseguradoId { get; set; }


    }
}

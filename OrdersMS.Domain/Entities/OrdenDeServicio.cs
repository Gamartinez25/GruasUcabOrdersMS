using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Domain.Entities
{
    public class OrdenDeServicio:Base
    {
        public Guid Id { get;private set; }
        public DateTime Fecha { get; private    set; }
        public string DetallesIncidente { get; private set; }
        public string DireccionOrigen { get; private set; }
        public string DireccionDestino { get; private set; }
        public double? CantidadKmExtra { get; private set; }
        public double? CostoServiciosAdicionales { get; private set; }
        public double? CostoTotalKmExtra { get; private set; }
        public double CostoTotal { get; private set; }
        public string NombreDenunciante { get; private set; }
        public string TipoDocumentoDenunciante { get; private set; }
        public string NumeroDocumentoDenunciante { get; private set; }

        public Guid PolizaAseguradoId { get; set; }  // Clave foránea a PolizaAsegurado
        public PolizaAsegurado? PolizaAsegurado { get; set; }  // Relación con PolizaAsegurado

        public Guid? Administrador { get; private set; }
        public Guid? Operador { get; private set; }
        public Guid? Vehiculo {  get; private set; }
        public ICollection<OrdenCostoAdicional>? OrdenCostosAdicionales { get; private set; }
        public OrdenDeServicio(Guid id,
                               DateTime fecha,
                               string detallesIncidente,
                               string direccionOrigen,
                               string direccionDestino,
                               double costoTotal,
                               string nombreDenunciante,
                               string tipoDocumentoDenunciante,
                               string numeroDocumentoDenunciante,
                               Guid polizaAseguradoId,
                               Guid? administrador = null,
                               Guid? operador = null,
                               Guid? vehiculo = null,
                               double? cantidadKmExtra = null,
                               double? costoServiciosAdicionales = null,
                               double? costoTotalKmExtra = null)
        {
            Id = id;
            Fecha = fecha;
            DetallesIncidente = detallesIncidente;
            DireccionOrigen = direccionOrigen;
            DireccionDestino = direccionDestino;
            CantidadKmExtra = cantidadKmExtra;
            CostoServiciosAdicionales = costoServiciosAdicionales;
            CostoTotalKmExtra = costoTotalKmExtra;
            CostoTotal = costoTotal;
            NombreDenunciante = nombreDenunciante;
            TipoDocumentoDenunciante = tipoDocumentoDenunciante;
            NumeroDocumentoDenunciante = numeroDocumentoDenunciante;
            PolizaAseguradoId = polizaAseguradoId;
            Administrador = administrador;
            Operador = operador;
            Vehiculo = vehiculo;
           
        }

        public OrdenDeServicio()
        {
        }
        
        public void ActualizarCostosServiciosAdicionales(double costoTotal)
        {
            CostoServiciosAdicionales=costoTotal;
        }
        public void ActualizarTotal (double costoTotal)
        {
           CostoTotal=costoTotal;
        }

    }

}

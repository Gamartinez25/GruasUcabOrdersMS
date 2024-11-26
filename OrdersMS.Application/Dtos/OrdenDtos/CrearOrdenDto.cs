using OrdersMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Dtos.OrdenDtos
{
    public class CrearOrdenDto
    {
        public string DetallesIncidente { get; private set; }
        public string DireccionOrigen { get; private set; }
        public string DireccionDestino { get; private set; }
        public double CantidadKmExtra { get; private set; }
        public string NombreDenunciante { get; private set; }
        public string TipoDocumentoDenunciante { get; private set; }
        public string NumeroDocumentoDenunciante { get; private set; }
        public double CostoTotalKmExtra { get; private set; }
        public double CostoTotal { get; private set; }

        // Relaciones
        public Guid PolizaAseguradoId { get; set; }
        public Guid? Administrador { get; private set; }
        public Guid? Operador { get; private set; }
        public Guid Vehiculo { get; private set; }
        public CrearOrdenDto(string detallesIncidente,
                             string direccionOrigen,
                             string direccionDestino,
                             string nombreDenunciante,
                             string tipoDocumentoDenunciante,
                             string numeroDocumentoDenunciante,
                             Guid polizaAseguradoId,
                             Guid vehiculo,
                             Guid? administrador=null,
                             Guid? operador=null,
                             double cantidadKmExtra = 0,
                             double costoTotalKmExtra=0,
                             double costoTotal=0)
        {
            DetallesIncidente = detallesIncidente;
            DireccionOrigen = direccionOrigen;
            DireccionDestino = direccionDestino;
            NombreDenunciante = nombreDenunciante;
            TipoDocumentoDenunciante = tipoDocumentoDenunciante;
            NumeroDocumentoDenunciante = numeroDocumentoDenunciante;
            PolizaAseguradoId= polizaAseguradoId;
            Administrador= administrador;
            Operador= operador;
            Vehiculo= vehiculo;
            CantidadKmExtra = cantidadKmExtra;
            CostoTotalKmExtra = costoTotalKmExtra;
            CostoTotal= costoTotal;
        }
    }

}

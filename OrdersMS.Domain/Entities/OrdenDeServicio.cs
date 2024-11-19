using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Domain.Entities
{
    public class OrdenDeServicio
    {
        public Guid Id { get;private set; }
        public DateTime Fecha { get; private    set; }
        public string DetallesIncidente { get; private set; }
        public string Direccion { get; private set; }
        public string Estatus { get; private set; }
        public double CantidadKmExtra { get; private set; }
        public double CostoServiciosAdicionales { get; private set; }
        public double CostoTotalKm { get; private set; }
        public double CostoTotal { get; private set; }
        public string NombreDenunciante { get; private set; }
        public string TipoDocumentoDenunciante { get; private set; }
        public string NumeroDocumentoDenunciante { get; private set; }

        // Relaciones
        public Guid PolizaAseguradoId { get; set; }  // Clave foránea a PolizaAsegurado
        public PolizaAsegurado PolizaAsegurado { get; set; }  // Relación con PolizaAsegurado

        public Guid Administrador { get; private set; }
        public Guid Operador { get; private set; }
        public ICollection<OrdenCostoAdicional> OrdenCostosAdicionales { get; private set; }
        
    }
}

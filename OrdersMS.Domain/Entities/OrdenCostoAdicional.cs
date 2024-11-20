using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Domain.Entities
{
    public class OrdenCostoAdicional:Base
    {
        public Guid OrdenDeServicioId { get; set; }
        public OrdenDeServicio OrdenDeServicio { get; set; }

        public Guid CostoAdicionalId { get; set; }
        public CostoAdicional CostoAdicional { get; set; }

        // Atributos adicionales en la relación
        public double Costo { get; set; }
        public string Estatus { get; set; } // Nuevo atributo
    }
}

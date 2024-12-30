using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Domain.Entities
{
    public class OrdenCostoAdicional:Base
    {
        public OrdenCostoAdicional(Guid idCostoOrden,
                                   Guid ordenDeServicioId,
                                   Guid costoAdicionalId,
                                   double costo,
                                   string estatus,
                                   string descripcion,
                                   int? Id,
                                   DateTime fechaCreacion,
                                   string? creadoPor,
                                   DateTime? fechaActualizacion,
                                   string? actualizadoPor):base(Id,fechaCreacion,creadoPor,fechaActualizacion, actualizadoPor)
        {
            IdCostoOrden = idCostoOrden;
            OrdenDeServicioId = ordenDeServicioId;
            CostoAdicionalId = costoAdicionalId;
            Costo = costo;
            Estatus = estatus;
            Descripcion = descripcion;
        }
        public OrdenCostoAdicional() { }

        public Guid IdCostoOrden {  get; set; }
        public Guid OrdenDeServicioId { get; set; }
        public OrdenDeServicio OrdenDeServicio { get; set; }

        public Guid CostoAdicionalId { get; set; }
        public CostoAdicional CostoAdicional { get; set; }

        // Atributos adicionales en la relación
        public double Costo { get; set; }
        public string Estatus { get; set; } // Nuevo atributo
        public string? Descripcion { get; set; } // Nuevo atributo
        public void ActualizarEstatus(string estatus) 
        { 
            Estatus = estatus;
        }


    }
}

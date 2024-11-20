using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Domain.Entities
{
    public class Tarifa:Base
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; }
        public decimal CostoBase { get;  private set; }
        public double DistanciaKm { get;  private set; }
        public double CostoPorKm { get;  private set; }
        public string Estatus { get; private  set; }

        // Relación
        public Poliza Poliza { get; private set; }

        
        public void ActualizarEstatus(string estatus)
        {
            Estatus = estatus;
        }
    }
}

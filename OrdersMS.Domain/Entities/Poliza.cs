using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Domain.Entities
{
    public class Poliza
    {
        public Guid Id { get;  private set; }
        public string Nombre { get;  private set; }
        public double Costo { get; private  set; }
        public string Descripcion { get;  private set; }

        // Relaciones
        public Tarifa Tarifa { get; private  set; }
        public PolizaAsegurado PolizaAsegurado { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Domain.Entities
{
    public class Poliza
    {
        public Poliza()
        {
        }

        public Poliza(Guid id, string nombre, double costo, string descripcion)
        {
            Id = id;
            Nombre = nombre;
            Costo = costo;
            Descripcion = descripcion;
        }
        public Guid Id { get;  private set; }
        public string Nombre { get;  private set; }
        public double Costo { get; private  set; }
        public string Descripcion { get;  private set; }

        // Relaciones
        public Tarifa Tarifa { get; private  set; }
        public Guid TarifaId { get; private set; }
        public PolizaAsegurado PolizaAsegurado { get; set; }
    }
}

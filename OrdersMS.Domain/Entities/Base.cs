using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Domain.Entities
{
    public class Base
    {
        public int? Id { get; set; }
        public  DateTime FechaCreacion  { get; set; }
        public string? CreadoPor { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string? ActualizadoPor { get; set; }

    }
}

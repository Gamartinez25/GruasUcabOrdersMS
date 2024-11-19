using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Domain.Entities
{
    public class CostoAdicional
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; }

        public ICollection<OrdenCostoAdicional> OrdenCostosAdicionales { get; set; }

    }

}

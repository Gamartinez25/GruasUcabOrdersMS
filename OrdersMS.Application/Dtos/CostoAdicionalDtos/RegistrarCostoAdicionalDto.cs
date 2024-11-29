using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Dtos.CostoAdicionalDtos
{
    public class RegistrarCostoAdicionalDto
    {
        public Guid IdOrden { get; private set; }
        public Guid IdCostoAdicional { get; private set; }
        public double Costo { get; private set; }
        public string? Descripcion {  get; private set; }
        public RegistrarCostoAdicionalDto(Guid idOrden, Guid idCostoAdicional, double costo, string? descripcion)
        {
            IdOrden = idOrden;
            IdCostoAdicional = idCostoAdicional;
            Costo = costo;
            Descripcion = descripcion;
        }

    }
}

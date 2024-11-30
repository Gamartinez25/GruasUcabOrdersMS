using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Dtos.CostoAdicionalDtos
{
    public class ModificarCostoAdicionalDto
    {
        public Guid Id { get;  private set; }
        public double Monto { get; private set; }
        public string Descripcion {  get; private set; }
        public ModificarCostoAdicionalDto(Guid id, double monto, string descripcion)
        {
            Id = id;
            Monto = monto;
            Descripcion = descripcion;
        }
    }
}

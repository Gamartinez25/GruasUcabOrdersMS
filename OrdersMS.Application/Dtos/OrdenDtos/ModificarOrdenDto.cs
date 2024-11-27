using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Dtos.OrdenDtos
{
    public class ModificarOrdenDto
    {
        public ModificarOrdenDto(Guid id, Guid vehiculo, double cantidadKmExtra=0, double costoTotalKmExtra=0,double costoTotal = 0)
        {
            Id=id;
            CantidadKmExtra=cantidadKmExtra;
            CostoTotal=costoTotal;
            CostoTotalKmExtra=costoTotalKmExtra;
            Vehiculo=vehiculo;
        }

        public Guid Id { get; private set; }
        public double CantidadKmExtra { get; private set; }
        public double CostoTotalKmExtra { get; private set; }
        public double CostoTotal { get; private set; }
        public Guid Vehiculo { get; private set; }
    }
}

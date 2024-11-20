using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Dtos.TarifaDtos
{
    public class CrearTarifaDto
    {

        public string Nombre { get; private set; }
        public double CostoBase { get; private set; }
        public double DistanciaKm { get; private set; }
        public double CostoPorKm { get; private set; }
        public CrearTarifaDto(string nombre,
                             double costoBase,
                             double distanciaKm,
                             double costoPorKm)
        {
            Nombre = nombre;
            CostoBase = costoBase;
            DistanciaKm = distanciaKm;
            CostoPorKm = costoPorKm;
        }
    }
}

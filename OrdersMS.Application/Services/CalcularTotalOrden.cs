
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Services
{
    public static class CalcularTotalOrden
    {
        
        public static  double CalcularTotalOrdenDeServicio(Tarifa tarifa,double? totalCostoKmExtra, double totalCostoAdicional)
        {
  
            double diferenciaCoberturaCostos = tarifa.CostoBase - totalCostoAdicional;
            double Subtotal= diferenciaCoberturaCostos > 0 ? 0 : Math.Abs(diferenciaCoberturaCostos);
            return (double)(Subtotal + totalCostoKmExtra);

        }
    }
}

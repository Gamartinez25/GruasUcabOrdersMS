
namespace OrdersMS.Application.Dtos.OrdenDtos
{
    public class ModificarOrdenDto
    {
        public ModificarOrdenDto(Guid id, Guid vehiculo, double cantidadKmExtra=0, double costoTotalKmExtra=0)
        {
            Id=id;
            CantidadKmExtra=cantidadKmExtra;
           
            CostoTotalKmExtra=costoTotalKmExtra;
            Vehiculo=vehiculo;
        }

        public Guid Id { get; private set; }
        public double CantidadKmExtra { get; private set; }
        public double CostoTotalKmExtra { get; private set; }
       
        public Guid Vehiculo { get; private set; }
    }
}

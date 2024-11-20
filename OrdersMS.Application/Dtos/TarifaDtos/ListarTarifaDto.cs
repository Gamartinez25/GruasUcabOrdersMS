

namespace OrdersMS.Application.Dtos.TarifaDtos
{
    public class ListarTarifaDto
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; }
        public double CostoBase { get; private set; }
        public double DistanciaKm { get; private set; }
        public double CostoPorKm { get; private set; }
        public ListarTarifaDto(Guid id,
                               string nombre,
                               double costoBase,
                               double distanciaKm,
                               double costoPorKm)
        {
            Id= id;
            Nombre= nombre; 
            CostoBase= costoBase;
            DistanciaKm= distanciaKm;
            CostoPorKm= costoPorKm;
        }
    }
}


namespace OrdersMS.Application.Dtos.OrdenDtos
{
    public class VehiculosDisponiblesAsignarOrdenDto
    {
        public Guid IdVehiculo { get; private set; }
        public string Tipo { get; private set; }
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public string Placa { get; private set; }
        public string Color { get; private set; }
        public string DistanciaGruaOrigen { get; private set; }
        public string DistanciaRutaTotal { get; private set; }
        public string TiempoGruaOrigen { get; private set; }
        public double CantidadDeKmExtra { get; private set; }
        public double RecargoPorKmExtra { get; private set; }
        public VehiculosDisponiblesAsignarOrdenDto(Guid idVehiculo,
                                                   string tipo,
                                                   string marca,
                                                   string modelo,
                                                   string placa,
                                                   string color,
                                                   string distanciaGruaOrigen,
                                                   string distanciaRutaTotal,
                                                   string tiempoGruaOrigen,
                                                   double cantidadDeKmExtra,
                                                   double recargoPorKmExtra
                                                   )
        {
            IdVehiculo = idVehiculo;
            Tipo = tipo;
            Marca = marca;
            Modelo = modelo;
            Color = color;
            Placa = placa;
            DistanciaGruaOrigen = distanciaGruaOrigen;
            DistanciaRutaTotal = distanciaRutaTotal;
            TiempoGruaOrigen= tiempoGruaOrigen;
            CantidadDeKmExtra= cantidadDeKmExtra;
            RecargoPorKmExtra= recargoPorKmExtra;
        }

        


    }
}

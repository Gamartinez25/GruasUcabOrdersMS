

namespace OrdersMS.Infrastructure.Mappers
{
    public class VehiculoDto
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string  Color { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public int DistanciaValor { get; set; }
        public string DistanciaTexto { get; set; }
        public int DuracionValor { get; set; }
        public string DuracionTexto { get; set; }

    }
}

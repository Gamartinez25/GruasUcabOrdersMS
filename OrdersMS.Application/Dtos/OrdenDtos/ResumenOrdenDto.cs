namespace OrdersMS.Application.Dtos.OrdenDtos
{
    public class ResumenOrdenDto
    {
        public ResumenOrdenDto(Guid id,
                               string direccionOrigen,
                               string direccionDestino,
                               string denunciante,
                               string estatus)
        {
            Id = id;
            DireccionOrigen = direccionOrigen;
            DireccionDestino = direccionDestino;
            Denunciante = denunciante;
            Estatus = estatus;
        }

        public Guid Id { get;  private set; }
        public string DireccionOrigen { get; private set; }
        public string DireccionDestino { get; private set; }
        public string Denunciante { get; private set; }
        public string  Estatus { get; private set; }


    }
}

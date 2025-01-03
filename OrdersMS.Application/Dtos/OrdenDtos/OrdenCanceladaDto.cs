namespace OrdersMS.Application.Dtos.OrdenDtos
{
    public class OrdenCanceladaDto
    {
        public OrdenCanceladaDto(Guid id,
                                 string denunciante,
                                 string direccionOrigen,
                                 string direccionDestino,
                                 string fecha,
                                 string estatus)
        {
            Id = id;
            Denunciante = denunciante;
            DireccionOrigen = direccionOrigen;
            DirecionDestino = direccionDestino;
            Fecha = fecha;
            Estatus = estatus;
        }

        public Guid Id { get;  private set; }
        public string Denunciante { get; private set; }
        public  string DireccionOrigen {  get; private set; }
        public string DirecionDestino { get; private set; }
        public string Fecha { get; private set; }
        public string Estatus { get; private set; }
    }
}
